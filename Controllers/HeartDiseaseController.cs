using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace esprit_backend2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartDiseaseController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public HeartDiseaseController()
        {
            _httpClient = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> SendAndGetHealthDisease([FromBody] HeartDiseaseDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Input DTO cannot be null.");
            }

            var requestBody = new
            {
                Age = dto.Age,
                Sex = dto.Sex ? 1 : 0,
                ChestPainType = dto.ChestPainType,
                RestingBP = dto.RestingBP,
                Cholesterol = dto.Cholesterol,
                FastingBS = dto.FastingBS,
                RestingECG = dto.RestingECG,
                MaxHR = dto.MaxHR,
                ExerciseAngina = dto.ExerciseAngina ? 1 : 0,
                Oldpeak = dto.Oldpeak
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://127.0.0.1:5000/predict", content);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error calling Python service.");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return Ok(responseString);
        }
    }
}
