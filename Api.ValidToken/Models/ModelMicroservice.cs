using System;
using System.Collections.Generic;

namespace Api.ValidToken.Models
{

    public class ConfigOutput
    {
        public List<ReRoutes> ReRoutes { get; set; }
        public GlobalConfiguration GlobalConfiguration { get; set; }

    }

    public class GlobalConfiguration
    {

    }

    public class ReRoutes
    {
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }
        public List<DownstreamHostAndPortsInput> DownstreamHostAndPorts { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<String> UpstreamHttpMethod { get; set; }
        public AuthenticationOptionsInput AuthenticationOptions { get; set; }
        public RateLimitOptionsInput RateLimitOptions { get; set; }
        public FileCacheOptionsInput FileCacheOptions { get; set; }

    }


    public class ReRoutesOutput
    {
        public int Id { get; set; }
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<DownstreamHostAndPortsInput> DownstreamHostAndPorts { get; set; }
        public List<String> UpstreamHttpMethod { get; set; }
        public AuthenticationOptionsInput AuthenticationOptions { get; set; }
        public RateLimitOptionsInput RateLimitOptions { get; set; }
        public FileCacheOptionsInput FileCacheOptions { get; set; }

    }

    public class AllowedScopes
    {
        public int Id { get; set; }
        public int AuthenticationOptionsId { get; set; }
    }

    public class AuthenticationOptions
    {
        public int Id { get; set; }
        public string AuthenticationProviderKey { get; set; }
        public int ReRoutesId { get; set; }
        public List<AllowedScopes> AllowedScopes { get; set; }
    }


    public class ClientWhitelist
    {
        public int Id { get; set; }
        public int RateLimitOptionsId { get; set; }
    }

    public class DownstreamHostAndPorts
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Post { get; set; }
        public int ReRoutesId { get; set; }
    }

    public class FileCacheOptions
    {
        public int Id { get; set; }
        public int TtlSeconds { get; set; }
        public string Region { get; set; }
        public int ReRoutesId { get; set; }
    }

    public class RateLimitOptions
    {
        public int Id { get; set; }
        public bool EnableRateLimiting { get; set; }
        public string Period { get; set; }
        public int PeriodTimespan { get; set; }
        public int Limit { get; set; }
        public int ReRoutesId { get; set; }
        public List<ClientWhitelist> ClientWhitelist { get; set; }
    }

    public class UpstreamHttpMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReRoutesId { get; set; }
    }

    public class ReRoutesInput
    {
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<DownstreamHostAndPortsInput> DownstreamHostAndPorts { get; set; }
        public List<UpstreamHttpMethodInput> UpstreamHttpMethod { get; set; }
        public AuthenticationOptionsInput AuthenticationOptions { get; set; }
        public RateLimitOptionsInput RateLimitOptions { get; set; }
        public FileCacheOptionsInput FileCacheOptions { get; set; }

    }

    public class AllowedScopesInput
    {

    }

    public class AuthenticationOptionsInput
    {
        public string AuthenticationProviderKey { get; set; }
        public List<AllowedScopesInput> AllowedScopes { get; set; }
    }


    public class ClientWhitelistInput
    {
    }

    public class DownstreamHostAndPortsInput
    {
        public string Host { get; set; }
        public int Post { get; set; }
    }

    public class FileCacheOptionsInput
    {
        public int TtlSeconds { get; set; }
        public string Region { get; set; }
    }

    public class RateLimitOptionsInput
    {
        public List<ClientWhitelistInput> ClientWhitelist { get; set; }
        public bool EnableRateLimiting { get; set; }
        public string Period { get; set; }
        public int PeriodTimespan { get; set; }
        public int Limit { get; set; }
    }

    public class UpstreamHttpMethodInput
    {
        public string Name { get; set; }
    }

    public class GetReRoutesInput
    {
        public int Id { get; set; }
    }

    public class testInput
    {
        public int ReRoutesId { get; set; }
    }


    public class AppsettingToken
    {
        public int Id { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string UrlPort { get; set; }
        public SettingToken Setting { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class AppsettingTokenOutput
    {
        public ConnectionStringsInput ConnectionStrings { get; set; }
        public LoggingInput Logging { get; set; }
        public string UrlPort { get; set; }
        public SettingTokenInput Setting { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class AppsettingTokenInput
    {
        public ConnectionStringsInput ConnectionStrings { get; set; }
        public LoggingInput Logging { get; set; }
        public string UrlPort { get; set; }
        public SettingTokenInput Setting { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class AppsettingGateway
    {
        public int Id { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string UrlPort { get; set; }
        public GatewayInput Setting { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class AppsettingGatewayOutput
    {
        public ConnectionStringsInput ConnectionStrings { get; set; }
        public LoggingInput Logging { get; set; }
        public string UrlPort { get; set; }
        public GatewayInput Setting { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class AppsettingGateWayInput
    {
        public ConnectionStringsInput ConnectionStrings { get; set; }
        public LoggingInput Logging { get; set; }
        public string UrlPort { get; set; }
        public GatewayInput Setting { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class ConnectionStrings
    {
        public int Id { get; set; }
        public string DefaultConnection { get; set; }
        public string AppsettingId { get; set; }

    }

    public class Logging
    {
        public int Id { get; set; }
        public LogLevel LogLevel { get; set; }
        public string AppsettingId { get; set; }
    }

    public class LogLevel
    {
        public int Id { get; set; }
        public string Default { get; set; }
        public string LoggingId { get; set; }
    }

    public class SettingToken
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecrets { get; set; }
        public string AllowedScopes { get; set; }
        public string Address { get; set; }
        public string AppsettingId { get; set; }
    }

    public class ConnectionStringsInput
    {
        public string DefaultConnection { get; set; }

    }

    public class LoggingInput
    {
        public LogLevelInput LogLevel { get; set; }
    }

    public class LogLevelInput
    {
        public string Default { get; set; }
    }

    public class SettingTokenInput
    {
        public string ClientId { get; set; }
        public string ClientSecrets { get; set; }
        public string AllowedScopes { get; set; }
        public string Address { get; set; }
    }


    public class GetAppsettingInput
    {
        public string AllowedScopes { get; set; }
    }

    public class Gateway
    {
        public int Id { get; set; }
        public string KeyPrivite { get; set; }
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string AppsettingId { get; set; }
    }

    public class GatewayInput
    {
        public string KeyPrivite { get; set; }
        public string Authority { get; set; }
        public string ApiName { get; set; }
    }


    public class Resource
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
