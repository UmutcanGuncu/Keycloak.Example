using System.Text.Json.Serialization;

namespace Keycloak.Example.Dtos;

public class LoginUserResultDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonPropertyName("refresh_expires_in")]
    public int RefreshExpiresIn { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}
/*
{
       "access_token": "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJqdWtqMHFvbXhZVlNDQmd1dUFualVySWZZdlA3bkhySVNBZ0QtVUhlM0VRIn0.eyJleHAiOjE3NjQ5MjI1NjYsImlhdCI6MTc2NDkyMjI2NiwianRpIjoib25ydHJvOmM0YzRjODQxLTlhN2YtYmJlZC05OTQwLWIyNGU2MGM4NDI1MSIsImlzcyI6Imh0dHA6Ly8xMjcuMC4wLjE6ODA4MC9yZWFsbXMvVW11dFJlYWxtIiwiYXVkIjoiYWNjb3VudCIsInN1YiI6Ijk3YmIxNzM2LWNlNzMtNDdhZi1iZDFlLWU3NDBiYzUxZTU5YiIsInR5cCI6IkJlYXJlciIsImF6cCI6IkRlbmVtZUNsaWVudCIsInNpZCI6IjY1NDJmYTJhLTdiMDUtOTYxMS1mM2NiLWRjZjU3NDNlNDIyNiIsImFjciI6IjEiLCJhbGxvd2VkLW9yaWdpbnMiOlsiLyoiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbIm9mZmxpbmVfYWNjZXNzIiwiZGVmYXVsdC1yb2xlcy11bXV0cmVhbG0iLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoicHJvZmlsZSBlbWFpbCIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYW1lIjoic3RyaW5nIHN0cmluZyIsInByZWZlcnJlZF91c2VybmFtZSI6Imd1bmN1IiwiZ2l2ZW5fbmFtZSI6InN0cmluZyIsImZhbWlseV9uYW1lIjoic3RyaW5nIiwiZW1haWwiOiJ1bXV0Y2FuZ3VuY3VAaWNsb3VkLmNvbSJ9.qfFMuYcHeTV98ZYSawAHNnQhxIP46mH6vPp0lMOg_MPibAqa9xjPPziepjyQ2QSffl1K95IKiXTlOpjoqBA_OXRCl7QGOV6iAAlS2C6c_ZGaIapurV8fdn-m8olVqNwddkCMbecGHSQNFsGdZOEmDmcIsnKyxovhClg6Txg8pj_cghURBzQ0wdkWCCUPwsy1EZ3kaPiU4woqjlCDiaURpPn43JHQcefnFxtxhKga_WBexUvhclKvZlknpQGQCw4v090QhdMyo3jiXI904zrFSp2Qj01mfCZNiPBALZLVPzNmuDNDgo6DOLTshPvbO2q6KLkH53FQLTilrogs-I_eAw",
       "expires_in": 300,
       "refresh_expires_in": 1800,
       "refresh_token": "eyJhbGciOiJIUzUxMiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJmZWE4MjcxMC04OGUwLTQ5NjgtODI5MS1lZGQ3MjFmMDFlMmUifQ.eyJleHAiOjE3NjQ5MjQwNjYsImlhdCI6MTc2NDkyMjI2NiwianRpIjoiNjMxYTNlZmMtYzJhYi0wYzU3LWZhMjAtZjYwYTA1YmZmNDUyIiwiaXNzIjoiaHR0cDovLzEyNy4wLjAuMTo4MDgwL3JlYWxtcy9VbXV0UmVhbG0iLCJhdWQiOiJodHRwOi8vMTI3LjAuMC4xOjgwODAvcmVhbG1zL1VtdXRSZWFsbSIsInN1YiI6Ijk3YmIxNzM2LWNlNzMtNDdhZi1iZDFlLWU3NDBiYzUxZTU5YiIsInR5cCI6IlJlZnJlc2giLCJhenAiOiJEZW5lbWVDbGllbnQiLCJzaWQiOiI2NTQyZmEyYS03YjA1LTk2MTEtZjNjYi1kY2Y1NzQzZTQyMjYiLCJzY29wZSI6IndlYi1vcmlnaW5zIGJhc2ljIHByb2ZpbGUgZW1haWwgc2VydmljZV9hY2NvdW50IGFjciByb2xlcyJ9.bT3TmFwgDN8L6NFqTnVnePk4PuMs20OY7Sm0Iatl8zdNuBOKebRO2zJw5imdEiKjxGaLyJ4Y9ldNGmwCVzS2IA",
       "token_type": "Bearer",
       "not-before-policy": 0,
       "session_state": "6542fa2a-7b05-9611-f3cb-dcf5743e4226",
       "scope": "profile email"
   }
   */