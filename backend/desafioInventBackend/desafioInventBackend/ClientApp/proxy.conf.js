import { env } from 'process';

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
	env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : '   ';

export const PROXY_CONFIG = [
	{
		context: [
			"/equipamento-eletronico",
		],
		proxyTimeout: 10000,
		target: target,
		secure: false,
		headers: {
			Connection: 'Keep-Alive'
		}
	}
]
