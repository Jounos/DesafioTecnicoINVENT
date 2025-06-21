using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioInventBackend.Context;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service.Contract;

namespace DesafioInventBackend.Controller
{
    [Route("api/equipamento-eletronico")]
    [ApiController]
    public class EquipamentoEletronicoController : ControllerBase
    {

        private readonly IEquipamentoEletronicoService _equipamentoEletronicoService;

        public EquipamentoEletronicoController(IEquipamentoEletronicoService equipamentoEletronicoService)
        {
            this._equipamentoEletronicoService = equipamentoEletronicoService;
        }


    }
}
