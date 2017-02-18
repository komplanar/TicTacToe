using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TicTacToe.Models;
using TicTacToe.Repositories;
using System;

namespace TicTacToe
{
    public class GameController : Controller
    {
        private LoginService LS;
        private GameService GS;
        private HistoryService HS;

        public GameController()
        {
            IUnitOfWork _db = new EfUnitOfWork("tttDBConnection");
            LS = new LoginService(_db);
            GS = new GameService(_db);
            HS = new HistoryService(_db);

        }

        public GameController(LoginService ls, GameService gs, HistoryService hs)
        {
            LS = ls;
            GS = gs;
            HS = hs;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var userId = LS.Login(user);
                if (user.Name != "")
                {
                    Session["user.Name"] = user.Name;
                    Session["user.Id"] = userId;
                }
                return RedirectToAction("NewGame", new {Id = userId});
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session["user.Name"] = null;
            Session["user.Id"] = null;

            return RedirectToAction("Login");
        }

        public ActionResult NewGame(int? id)
        {
            Session["BotStart"] = !GS.IsPlayerFirst;
            if (Session["user.Name"] == null || Session["user.Id"] == null)
            {
                return RedirectToAction("Login");
            }

            var gameId = GS.NewGame(id);
            return RedirectToAction("Game", new {Id = gameId});
        }

        public ActionResult Game(int? id)
        {
            if (Session["user.Name"] == null || Session["user.Id"] == null || id == null)
            {
                return RedirectToAction("Login");
            }
            
            ViewBag.Id = id;
            
            if (Request.IsAjaxRequest() && !Request.Params["position"].IsNullOrWhiteSpace())
            {
                var position = int.Parse(Request.Params["position"]);
                ViewBag.Status = GS.Move(id, position);
                ViewBag.State = GS.gameLogic.CellState;

                return PartialView("_Game");
            }
            GS.IfBotMoveFirst(id);
            ViewBag.State = GS.gameLogic.CellState;
            Session["IsPlayerFirst"] = GS.IsPlayerFirst;

            return View();
        }

        public ActionResult History(int? id)
        {
            if (Session["user.Name"] == null || Session["user.Id"] == null || id == null)
                return RedirectToAction("Login");

            var userGames = HS.GetUserGame(id);

            if (userGames != null)
            {
                return View(userGames);
            }

            return View();
        }
    }
}