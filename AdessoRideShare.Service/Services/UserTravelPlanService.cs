using AdessoRideShare.Db.Entity;
using AdessoRideShare.Db.Enum;
using AdessoRideShare.Model.DataModel.UserTravelPlan;
using AdessoRideShare.Model.RequestModel.UserTravelPlan;
using AdessoRideShare.Model.ResponseModel.UserTravelPlan;
using AdessoRideShare.Repository.Interfaces;
using AdessoRideShare.Service.Interfaces;
using AdessoRideShare.Util;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdessoRideShare.Service.Services
{
    public class UserTravelPlanService : BaseService<UserTravelPlan>, IUserTravelPlanService
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        public UserTravelPlanService(IRepository<UserTravelPlan> repository, IUnitOfWork unitOfWork,
            ICityService cityService, IMapper mapper) : base(repository, unitOfWork)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        public UserTravelPlanInsertResponse CreateUserTravelPlan(UserTravelPlanInsertRequest requestModel)
        {
            UserTravelPlanInsertResponse responseModel = new UserTravelPlanInsertResponse();

            var userTravelPlan = _mapper.Map<UserTravelPlan>(requestModel);

            Insert(userTravelPlan);
            _unitOfWork.Save();

            responseModel.Message = $"Seyahat planı oluşturulmuştur.";

            return responseModel;
        }

        public SearchUserTravelPlanResponse SearchUserTravelPlans(SearchUserTravelPlanRequest requestModel)
        {
            SearchUserTravelPlanResponse response = new SearchUserTravelPlanResponse();

            var fromCity = _cityService.Search(x => x.RecordId == requestModel.FromCityId).FirstOrDefault();
            var toCity = _cityService.Search(x => x.RecordId == requestModel.ToCityId).FirstOrDefault();

            if (fromCity == null)
                throw new Exception("Başlangıç Şehri Bulunamamıştır.");

            if (toCity == null)
                throw new Exception("Varış Şehri Bulunamamıştır.");

            var travels = Search(x => x.TravelState == (int)ETravelState.Publish && x.FromCityId == requestModel.FromCityId && x.ToCityId == requestModel.ToCityId).ToList();
            var travelModels = _mapper.Map<List<UserTravelPlanModel>>(travels);

            Dictionary<string, CityCoordinateModel> coordinateMatris = new Dictionary<string, CityCoordinateModel>();
            coordinateMatris.Add("XMatris", new CityCoordinateModel{ Value1 = fromCity.XLocation >= toCity.XLocation ? fromCity.XLocation : toCity.XLocation, Value2 = fromCity.XLocation < toCity.XLocation ? fromCity.XLocation : toCity.XLocation });
            coordinateMatris.Add("YMatris", new CityCoordinateModel { Value1 = fromCity.YLocation >= toCity.YLocation ? fromCity.YLocation : toCity.YLocation, Value2 = fromCity.YLocation < toCity.YLocation ? fromCity.YLocation : toCity.YLocation });

            var additionalTravels = Search(x => !travels.Select(s=>s.RecordId).Contains(x.RecordId) &&  x.TravelState == (int)ETravelState.Publish &&
            ((x.FromCity.XLocation >= coordinateMatris["XMatris"].Value2 && x.FromCity.XLocation <= coordinateMatris["XMatris"].Value1)
            || (x.FromCity.YLocation >= coordinateMatris["YMatris"].Value2 && x.FromCity.YLocation <= coordinateMatris["YMatris"].Value1))).ToList();

            response.UserTravelPlans = travelModels;
            response.AdditionalTravelPlans = _mapper.Map<List<UserTravelPlanModel>>(additionalTravels);

            return response;
        }

        public TravelPlanJoinResponse JoinTravelPlan(TravelPlanJoinRequest requestModel)
        {
            TravelPlanJoinResponse response = new TravelPlanJoinResponse();

            var travelPlan = Get(requestModel.TravelPlanId);

            if (travelPlan == null)
                throw new Exception("Seyahat planı bulunamadı.");

            if (travelPlan.TravelState != (int)ETravelState.Publish)
                throw new Exception($"Seçilen seyahat yayında değildir.");

            if(travelPlan.UserId == requestModel.UserId)
                throw new Exception($"Oluşturduğunuz seyahat planı için katılma isteği gönderemezsiniz.");

            if (travelPlan.Capacity <= travelPlan.TravelPlanMemberships.Count)
                throw new Exception($"Seyahat planı maksimum kişiye ulaşmıştır. Katılma isteği gönderemezsiniz.");

            travelPlan.TravelPlanMemberships.Add(new TravelPlanMembership
            {
                TravelPlanId = travelPlan.RecordId,
                UserId = requestModel.UserId,
                RecordState = ERecordState.Added
            });
            Update(travelPlan);
            _unitOfWork.Save();

            return response;
        }

        public UserTravelPlanChangeStateResponse UserTravelPlanChangeState(UserTravelPlanChangeStateRequest requestModel)
        {
            UserTravelPlanChangeStateResponse response = new UserTravelPlanChangeStateResponse();

            var travelPlan = Get(requestModel.TravelPlanId);
            if (travelPlan == null || travelPlan.UserId != requestModel.UserId)
                throw new Exception("Seyahat planı bulunamadı.");

            if(travelPlan.TravelState == requestModel.TravelState)
                throw new Exception("Seyahat plan durumu aynıdır.");

            
            var travelPlanState = Converter.GetEnumValue<ETravelState>(requestModel.TravelState);

            travelPlan.TravelState = (int)travelPlanState;
            Update(travelPlan);

            _unitOfWork.Save();

            response.Message = $"Seyahat Planı Durumu Güncellenmiştir. Son Durum : {travelPlanState}";

            return response;
        }


    }
}
