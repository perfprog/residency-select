using PPI.Core.Domain.Abstract;
using PPI.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PPI.Core.Domain.Entities;
using Microsoft.AspNet.Identity;
using System.Configuration;
using PPI.Core.Web.Infrastructure;

namespace PPI.Core.Web.Controllers
{
    public class AssessmentController : BaseController
    {
        public AssessmentController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Initiate()
        {
            try
            {
                if (Request.QueryString["arg"] != null)
                {
                    EncryptedQueryString args = new EncryptedQueryString(Request.QueryString["arg"]);
                    var personID = int.Parse(args["arg"].ToString());
                    var J3P_ID = args["arg1"].ToString();

                    IQueryable<Person> personList = UnitOfWork.IPersonRepository.AsQueryable();
                    IQueryable<AssessmentResponse> assessRespList = UnitOfWork.IAssessmentResponse.AsQueryable();

                    var CheckExists = (from person in personList.Where(e =>
                                             e.Id == personID && e.J3P_Id == J3P_ID)
                                       join assessResp in assessRespList
                                       on person.Id equals assessResp.PersonId
                                       select person);
                    if (CheckExists.Count() == 0)
                    {
                        HttpContext.Session["PersonID"] = args["arg"];
                        return RedirectToAction("Index", new { page = 0 });
                    }
                    else
                    {
                        return View("AssessmentError", null);
                    }
                }
                else
                    return Content("AssessmentError", null);
            }
            catch (Exception ex)
            {
                return View("AssessmentError", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [SessionAuthorizeAttribute]
        public ActionResult Index(int? page)
        {
            try
            {
                if (page == null || page == 0)
                    ViewData["Page"] = 0;
                else
                    ViewData["Page"] = page;

                if (ViewData["Page"].ToString() == "2")
                {
                    var model = new AssessmentViewModel();
                    var AssessQuestOptions = UnitOfWork.IAssessmentQuestionOption.AsQueryable();
                    model.AssessmentQuestionOptions = AssessQuestOptions.ToList();

                    return View("Index", model);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("AssessmentError", ex);
            }
        }

        /// <summary>
        /// Get the self assessment questions.
        /// </summary>
        /// <param name="page">page</param>
        /// <returns>ActionResult</returns>     
        [SessionAuthorizeAttribute]
        public ActionResult GetAssessmentQuestions(int? page)
        {
            try
            {
                ViewData["Page"] = string.Empty;

                var model = new AssessmentViewModel();
                int totalRecords = 0;
                var AssessQuestions = UnitOfWork.IAssessmentQuestion.AsQueryable();

                var PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["AssessQuestGridPageSize"]);

                totalRecords = AssessQuestions.Count();
                Decimal TotalPages = Decimal.Divide(totalRecords, PageSize);
                PagingInfo pagingInfo = new PagingInfo { CurrentPage = page.GetValueOrDefault(1), PageCount = Convert.ToInt32(Math.Ceiling(TotalPages)), TotalRecords = totalRecords, PageSize = PageSize };
                pagingInfo.LastPage = totalRecords / pagingInfo.PageSize;

                model.AssessmentQuestions = AssessQuestions.ToList()
                                         .Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize)
                                         .Take(pagingInfo.PageSize);

                var AssessQuestOptions = UnitOfWork.IAssessmentQuestionOption.AsQueryable();
                model.AssessmentQuestionOptions = AssessQuestOptions.ToList();

                model.PagingInfo = pagingInfo;

                ViewData["SelectList"] = HttpContext.Session["SelectList"] ?? new Dictionary<string, string>();

                return View("Index", model);
            }
            catch (Exception ex)
            {
                return View("AssessmentError", ex);
            }
        }

        /// <summary>
        /// Retain the selected answers while pagination.
        /// Assign the selected answers to session.
        /// </summary>
        /// <param name="isChecked"></param>
        /// <param name="RadioBtnid"></param>
        /// <param name="AnswerSelected"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [SessionAuthorizeAttribute]
        public ActionResult Select(bool isChecked, String QuestionID, string AnswerSelected, string RadioBtnID)
        {
            try
            {
                var selectList = (Dictionary<string, string>)HttpContext.Session["SelectList"] ?? new Dictionary<string, string>();
                if (isChecked && !selectList.ContainsKey(QuestionID))
                {
                    selectList.Add(QuestionID, string.Format("{0}~{1}", RadioBtnID, AnswerSelected));
                }
                else if (isChecked && selectList.ContainsKey(QuestionID))
                {
                    selectList[QuestionID] = string.Format("{0}~{1}", RadioBtnID, AnswerSelected);
                }
                else if (!isChecked && selectList.ContainsKey(QuestionID))
                {
                    selectList.Remove(QuestionID);
                }
                HttpContext.Session["SelectList"] = selectList;

                return Content("OK");
            }
            catch (Exception ex)
            {
                return View("AssessmentError", ex);
            }
        }

        /// <summary>
        /// Save assement answers for a user.
        /// </summary>
        /// <returns></returns>        
        [SessionAuthorizeAttribute]
        public ActionResult SaveAssessment()
        {
            try
            {
                var selectList = (Dictionary<string, string>)HttpContext.Session["SelectList"];

                AssessmentResponse Response = new AssessmentResponse();
                Response.PersonId = Convert.ToInt32(HttpContext.Session["PersonID"].ToString());
                Response.CreateDate = DateTime.Now;

                UnitOfWork.IAssessmentResponse.Add(Response);
                UnitOfWork.Commit();
                var Responseid = Response.Id;

                foreach (var Answers in selectList)
                {
                    AssessmentResponseQuestionOption AssessAnswers = new AssessmentResponseQuestionOption();
                    AssessAnswers.AssessmentResponseId = Responseid;
                    AssessAnswers.AssessmentQuestionId = Convert.ToInt32(Answers.Key);
                    AssessAnswers.AssessmentQuestionOptionId = Convert.ToInt32(Answers.Value.Split('~')[1]);

                    UnitOfWork.IAssessmentResponseQuestionOption.Add(AssessAnswers);
                }

                UnitOfWork.Commit();

                HttpContext.Session.Clear();
                HttpContext.Session.Abandon();
                return View("AssessCompleted");
            }
            catch (Exception ex)
            {
                return View("AssessmentError", ex);
            }
        }

        /// <summary>
        /// Return error page
        /// </summary>
        /// <returns>view</returns>
        public ActionResult Error()
        {
            try
            {
                return View("AssessmentError", null);
            }
            catch (Exception ex)
            {
                return View("AssessmentError", ex);
            }
        }
    }
}