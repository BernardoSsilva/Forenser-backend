using AutoMapper;
using ForenserBackend.Communication.Requests;
using ForenserBackend.Communication.Response.Detailed;
using ForenserBackend.Communication.Response.Short;
using ForenserBackend.Domain.entities;

namespace ForenserBackend.Application

{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            EntityToResponse();
            RequestToEntity();
        }


        public void EntityToResponse() {
            CreateMap<UserEntity, DetailedUserResponseJson>();
            CreateMap<UserEntity, ShortUserResponseJson>();
            CreateMap<ImageEntity, ShortImageResponseJson>();
            CreateMap<ImageEntity, DetailedImageResponseJson>();
            CreateMap<OccurrenceEntity, DetailedOccurenceResponseJson>();
            CreateMap<OccurrenceEntity, ShortOccurenceResponseJson>();
            CreateMap<PeopleEntity, ShortPeopleResponseJson>();
            CreateMap<PeopleEntity, DetailedPeopleResponseJson>();
            CreateMap<ReportEntity, DetailedReportResponseJson>();
            CreateMap<ReportEntity, ShortReportResponseJson>();
            CreateMap<ServiceScheduleEntity, DetailedServiceScheduleResponseJson>();
            CreateMap<ServiceScheduleEntity, ShortServiceScheduleResponseJson>();
            CreateMap<VehicleEntity, DetailedVehicleResponseJson>();
            CreateMap<VehicleEntity, ShortVehicleResponseJson>();
        }
        public void RequestToEntity() {
            CreateMap<UserRequestJson, UserEntity>();
            CreateMap<ImageRequestJson,ImageEntity>();
            CreateMap< OccurrenceRequestJson, OccurrenceEntity>();
            CreateMap< PeopleRequestJson, PeopleEntity > ();
            CreateMap< ReportRequestJson, ReportEntity>();
            CreateMap< ServiceScheduleRequestJson, ServiceScheduleEntity>();
            CreateMap<VehicleRequestJson, VehicleEntity>();
        }

    }
}
