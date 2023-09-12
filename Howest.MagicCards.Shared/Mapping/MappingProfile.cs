using AutoMapper;
using Howest.MagicCards.DAL.Entities;
using Howest.MagicCards.Shared.Dtos;

namespace Howest.MagicCards.Shared.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CardDetailDto, Card>();

        CreateMap<Card, CardListDto>()
            .ForMember(dest => dest.SetName, opt => opt.MapFrom(src => src.SetCodeNavigation.Name))
            .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.FullName))
            .ForMember(dest => dest.RarityName, opt => opt.MapFrom(src => src.RarityCodeNavigation.Name))
            .ForMember(dest => dest.CardTypeShortHands, opt => opt.MapFrom(src => src.CardTypes.Select(ct => ct.Type.Name)))
            .ForMember(dest => dest.OriginalImageUrl, opt => opt.MapFrom(src => src.OriginalImageUrl))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

        CreateMap<Card, CardDetailDto>()
            .IncludeBase<Card, CardListDto>() 
            .ForMember(dest => dest.ManaCost, opt => opt.MapFrom(src => src.ManaCost))
            .ForMember(dest => dest.ConvertedManaCost, opt => opt.MapFrom(src => src.ConvertedManaCost))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Flavor, opt => opt.MapFrom(src => src.Flavor))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Power, opt => opt.MapFrom(src => src.Power))
            .ForMember(dest => dest.Toughness, opt => opt.MapFrom(src => src.Toughness))
            .ForMember(dest => dest.Layout, opt => opt.MapFrom(src => src.Layout))
            .ForMember(dest => dest.MultiverseId, opt => opt.MapFrom(src => src.MultiverseId))
            .ForMember(dest => dest.OriginalImageUrl, opt => opt.MapFrom(src => src.OriginalImageUrl))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.OriginalText, opt => opt.MapFrom(src => src.OriginalText))
            .ForMember(dest => dest.OriginalType, opt => opt.MapFrom(src => src.OriginalType))
            .ForMember(dest => dest.MtgId, opt => opt.MapFrom(src => src.MtgId))
            .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.Variations));
    }
}
