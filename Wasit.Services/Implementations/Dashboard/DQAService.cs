using Microsoft.EntityFrameworkCore;
using Wasit.Context;
using Wasit.Core.Entities.SettingTables;
using Wasit.Core.Entities.UserTables;
using Wasit.Services.Interfaces.Dashboard;
using Wasit.Services.ViewModels.City;
using Wasit.Services.ViewModels.FixedPages;

namespace Wasit.Services.Implementations.Dashboard
{
    public class DQAService : IDQAService
    {
        private readonly ApplicationDbContext _context;
        public DQAService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<(bool isSuccess, string message)> CreateQA(QuestionsAndAnswersViewModel model)
        {
            var questionsClient = new QuestionsClient
            {
                Question = model.QuestionAr,
                QuestionEn = model.QuestionEn,
                Answer = model.AnswerAr,
                AnswerEn = model.AnswerEn
            };

            _context.QuestionsClient.Add(questionsClient);
            await _context.SaveChangesAsync();
            return (true, "تمت الاضافة بنجاح");
        }

        public async Task<List<QuestionsAndAnswersViewModel>> GetQA()
        {

            var data = await _context.QuestionsClient
                .AsNoTracking()
                .Select(x => new QuestionsAndAnswersViewModel
                {
                    Id = x.Id,
                    QuestionAr = x.Question,
                    AnswerAr = x.Answer,
                    QuestionEn = x.QuestionEn,
                    AnswerEn = x.AnswerEn,
                }).ToListAsync();
            return data;
        }




        //public async Task<(bool isSuccess, string message)> RemoveCity(long id)
        // {
        //     var city = await _context.Cities.SingleOrDefaultAsync(x => x.Id == id);
        //     if (city is null)
        //         return (false, "لم يتم العثور علي المدينة");


        //     var hasUsers = _context.Users
        //      .Any(u => u.Region.CityId == id);  


        //     if (hasUsers)
        //     {
        //         return (false, "لايمكن حذف المدينة");
        //     }

        //     city.IsDeleted = true;
        //     _context.Cities.Update(city);
        //     await _context.SaveChangesAsync();
        //     return (true, "تم الحذف بنجاح");
        // }
    }
}
