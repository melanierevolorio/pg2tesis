const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:19857';

//const target = 'https://localhost:5003';

const PROXY_CONFIG = [
  {
    context: [
      "/test",
      "/bff",
      "/signin-oidc",
      "/signout-callback-oidc",
      "/connect",
      "/oauth",
      "/Identity",
      "/remote",
      "/remote/user",
      "/.well-known",
      "/_framework"
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
