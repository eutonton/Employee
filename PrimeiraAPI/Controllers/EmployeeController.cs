using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Model;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);
            
            _employeeRepository.Add(employee);
            return Ok();
        }

        //[Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            if (string.IsNullOrEmpty(employee.photo) || !System.IO.File.Exists(employee.photo))
            {
                return NotFound("Foto não encontrada.");
            }

            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);
           

            return File(dataBytes, "image/png");
        }

             
        

        //[Authorize]
        [HttpGet]
        [HttpGet]
        public IActionResult Get([FromQuery] int pageNumber, [FromQuery] int pageQuantity)
        {
            _logger.Log(LogLevel.Error, "Teve um erro");

            //throw new Exception("Erro de teste");

            var employees = _employeeRepository.Get(pageNumber, pageQuantity);

            _logger.LogInformation("Teste");

            return Ok(employees);
        }

    }
}
