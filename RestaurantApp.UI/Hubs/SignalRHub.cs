using Microsoft.AspNetCore.SignalR;
using RestaurantApp.Infrastructure.Context;
using RestaurantApp.Domain.Enums;

namespace RestaurantApp.UI.Hubs;

public class SignalRHub:Hub
{
    private readonly AppDbContext _context;

    public SignalRHub(AppDbContext context)
    {
        _context = context;
    }

    public async Task SendCategoryCount()
    {

        var value = _context.Categories.Count(c => c.Status != Status.Deleted);
        await Clients.All.SendAsync("ReceiveCategoryCount", value);
    }

    public async Task SendProductCount()
    {
        var value = _context.Products.Count(p => p.Status != Status.Deleted);
        await Clients.All.SendAsync("ReceiveProductCount", value);
    }

    public async Task SendCampaignCount()
    {
        var value = _context.Campaigns.Count(c => c.Status != Status.Deleted);
        await Clients.All.SendAsync("ReceiveCampaignCount", value);
    }

    public async Task SendIngredientCount()
    {
        var value = _context.Ingredients.Count(i => i.Status != Status.Deleted);
        await Clients.All.SendAsync("ReceiveIngredientCount", value);
    }
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

}
