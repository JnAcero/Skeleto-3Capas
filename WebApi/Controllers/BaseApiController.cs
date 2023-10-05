using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        public BaseApiController(IUnitOfWork unitOfWork, IMapper mapper)
        {
             _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        
    }
}