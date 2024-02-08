
using System;
using HotelBooking.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace HotelBooking.Extensions;

public static class ElasticSearchExtensions
{
	public static void AddElasticSearch(
		this IServiceCollection service, IConfiguration configuration
		)
	{
		var url = configuration["ElasticSearch:Url"];
		var defaultIndex = configuration["ElasticSearch:Index"];

		var settings = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);
		AddDefaultMapping(settings);

		var client = new ElasticClient(settings);
		service.AddSingleton<IElasticClient>(client);

		CreateIndex(client, defaultIndex);
	}


	private static void AddDefaultMapping(ConnectionSettings settings)
	{
		settings.DefaultMappingFor<Hotel>(hotel =>
		hotel
		.Ignore(x => x.CreatorId));
	}

	private static void CreateIndex(IElasticClient client, string indexName)
	{
		if (!client.Indices.Exists(indexName).Exists)
		{
			try
			{
				client.Indices.Create(indexName, i => i.Map<Hotel>(x => x.AutoMap()));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}

