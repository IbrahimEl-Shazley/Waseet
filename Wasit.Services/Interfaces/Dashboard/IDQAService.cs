using Wasit.Services.ViewModels.City;
using Wasit.Services.ViewModels.FixedPages;

namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDQAService
    {
        Task<(bool isSuccess, string message)> CreateQA(QuestionsAndAnswersViewModel model);
        Task<List<QuestionsAndAnswersViewModel>> GetQA();

      //  Task<(bool isSuccess, string message)> RemoveCity(long id);

    }
}
