// This script sets up HTTPS for the application using the ASP.NET Core HTTPS certificate
const fs = require('fs');
const spawn = require('child_process').spawn;
const path = require('path');

const baseFolder =
  process.env.APPDATA && process.env.APPDATA !== ''
    ? path.join(process.env.APPDATA, 'ASP.NET', 'https')
    : path.join(process.env.HOME, '.aspnet', 'https');

// Obtem o nome do certificado a partir do argumento --name= ou do package.json (npm_package_name)
const certificateArg = process.argv
  .map(arg => arg.match(/--name=(?<value>.+)/i))
  .filter(Boolean)[0];
const certificateName = certificateArg ? certificateArg.groups.value : process.env.npm_package_name;

if (!certificateName) {
  console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.');
  process.exit(-1);
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

// Cria a pasta baseFolder se não existir
if (!fs.existsSync(baseFolder)) {
  try {
    fs.mkdirSync(baseFolder, { recursive: true });
    console.log(`Created folder: ${baseFolder}`);
  } catch (err) {
    console.error(`Failed to create folder ${baseFolder}:`, err);
    process.exit(-1);
  }
}

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  // Executa o comando para exportar os certificados no formato PEM sem senha
  const dotnet = spawn('dotnet', [
    'dev-certs',
    'https',
    '--export-path',
    certFilePath,
    '--format',
    'Pem',
    '--no-password',
  ], { stdio: 'inherit' });

  dotnet.on('exit', (code) => {
    process.exit(code);
  });
} else {
  // Certificados já existem, finaliza normalmente
  process.exit(0);
}
