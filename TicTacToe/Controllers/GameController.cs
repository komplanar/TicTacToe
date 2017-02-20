using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using TicTacToe.Models;
using TicTacToe.Repositories;
namespace TicTacToe
{
    public class GameController : Controller
    {
        private LoginService loginService;
        private GameService gameService;
        private HistoryService historyService;

        public GameController()
        {
            IUnitOfWork _db = new EfUnitOfWork("tttDBConnection");
            loginService = new LoginService(_db);
            gameService = new GameService(_db);
            historyService = new HistoryService(_db);
        }

        public GameController(LoginService ls, GameService gs, HistoryService hs)
        {
            loginService = ls;
            gameService = gs;
            historyService = hs;
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
                var userId = loginService.Login(user);
                Session["user.Name"] = user.Name;
                Session["user.Id"] = userId;
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
            if (Session["user.Name"] == null || Session["user.Id"] == null)
            {
                return RedirectToAction("Login");
            }

            var gameId = gameService.NewGame(id);
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
                ViewBag.Status = gameService.Move(id, position);
                ViewBag.State = gameService.gameLogic.CellState;

                return PartialView("_Game");
            }

            gameService.IfBotMoveFirst(id);
            ViewBag.State = gameService.gameLogic.CellState;
            Session["IsPlayerFirst"] = gameService.IsPlayerFirst;

            return View();
        }

        public ActionResult History(int? id)
        {
            if (Session["user.Name"] == null || Session["user.Id"] == null || id == null)
            {
                return RedirectToAction("Login");
            }
            var userGames = historyService.GetUserGame(id);

            if (userGames != null)
            {
                return View(userGames);
            }
            return View();
        }
    }
}