# AutenticacaoJWT_dio

O projeto é uma aplicação de console em C# que realiza a autenticação de um usuário através de um nome de usuário e senha. Quando o usuário é autenticado com sucesso, um token JWT (JSON Web Token) é gerado. Em seguida, usa esse token para enviar um e-mail após a autenticação. O JWT contém informações sobre o usuário autenticado e é assinado digitalmente para garantir sua integridade.

O projeto demonstra conceitos importantes, como autenticação, geração de tokens JWT e envio de e-mail usando a biblioteca SmtpClient. Note que as credenciais do servidor SMTP e a chave secreta do JWT são fornecidas como exemplos e devem ser configuradas adequadamente para um ambiente de produção.
