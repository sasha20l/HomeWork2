using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_1_temperature.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TController : ControllerBase
    {
        private readonly WeatherForecastStorage holder;

        public TController(WeatherForecastStorage _holder)
        {
            holder = _holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] int temperature)//Дата назначается автоматически в классе Temperature
        {
            holder.CreateTemperature(temperature);
            return Ok();
        }

        [HttpGet("read/ from /{fromDate}/to/{toDate}")]
        public IActionResult Read([FromRoute]TimeSpan fromDate, [FromRoute] TimeSpan toDate)
        {
            foreach (TemperatureObject temperatureO in holder.temperatureObjectValue)
            {
                DateTime dtFrom = DateTime.Parse(fromDate.ToString());
                DateTime dtTo = DateTime.Parse(toDate.ToString());

                if (temperatureO.Time > dtFrom && temperatureO.Time < dtTo)
                {
                    return Ok(temperatureO);
                }
                else
                {
                }
                
            }
            return Ok();

        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime stringsTimeToUpdate, [FromQuery] int newTemp)
        {
            foreach(TemperatureObject temperatureO in holder.temperatureObjectValue)
            {
                if (temperatureO.Time == stringsTimeToUpdate)
                    temperatureO.TemperatureC = newTemp;
            }

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime stringsToDelete)
        {
            foreach (TemperatureObject temperatureO in holder.temperatureObjectValue)
            {
                if (temperatureO.Time == stringsToDelete)
                    holder.temperatureObjectValue.Remove(temperatureO);
            }
            return Ok();
        }
    }
}
