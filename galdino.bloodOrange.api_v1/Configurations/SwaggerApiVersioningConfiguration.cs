using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace galdino.bloodOrange.api_v1.Configurations
{
    public class SwaggerApiVersioningConfiguration
    {
        public static void Register(IServiceCollection app)
        {
            app.AddSwaggerGen(
                options =>
                {
                    AdicionarVersoes(options);

                    options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                    options.CustomSchemaIds(o => o.FullName);

                    options.IncludeXmlComments(XmlCommentsFilePath, true);
                    options.DescribeAllEnumsAsStrings();
                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "Blood Orange FCamara: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });

                    var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", new string[] { }},
                    };

                    options.AddSecurityRequirement(security);
                });
        }

        private static void AdicionarVersoes(SwaggerGenOptions options)
        {
            options.SwaggerDoc($"v1.0", new Info()
            {
                Title = $"Blood Orange FCamara - API - v1.0",
                Version = "1.0",
                Description = @"1) EXPLIQUE COM SUAS PALAVRAS O QUE É DOMAIN DRIVEN DESIGN E SUA IMPORTÂNCIA NA ESTRATÉGIA DE DESENVOLVIMENTO DE SOFTWARE.  
                                <b>
                                    R: O patterns Drive Domain Design ou DDD, apesar de ser um tanto complexa sua implementação,apos implementado ajuda muito na manutenção do sistema, 
                                    principalmente quando se tem equipe rotativa(sempre chegando gente nova na empresa ou projeto). Com isso conseguimos ter um padrão no projeto qualquer desenvolvedor que chegar se encontra na solution e em todas as camadas estão em seu devido lugar. Afinal quem nao quer trabalhar em um peojeto organizado ;-)
                                    Por isso que nao minha opnião o principal ganho no DDD é o padrão nos projetos que contem esse pattern.
                                </b>
                                </br>
                                2) EXPLIQUE COM SUAS PALAVRAS O QUE É E COMO FUNCIONA UMA ARQUITETURA BASEADA EM MICROSERVICES. EXPLIQUE GANHOS COM ESTE MODELO E DESAFIOS EM SUA IMPLEMENTAÇÃO. 
                                <b>
                                    R: Um microservico baseia se em desaclopar tudo que for possivel de sua aplicação na maioria das vezes requests que tem um payload muito grande, onde iria honerar muito processamento do seu servidor,
                                       assim podemos [quebrar] a aplicao para que não fique lenta e tenha mais processamento. O maior desafio de implementar o microseriviço é a quebra de paradigma, até pouco tempo não falariamos em retirar
                                       partes de suma importancia da nossa aplicação. O maior ganho é performace ao enviar uma execusao para uma fila sua aplicação fica livre pra continuar executando suas tarefas. 
                                       O maior desafio de implementar um microserviço é o particionamento de sua aplicação uma vez que se isso não for bem feito será muito dificil implementar um microserviço.
                                       Ganho, o principal e que todos nos Developer buscamos que é sempre o desenpenho das aplicações.
                                </b> 
                                3) EXPLIQUE QUAL A DIFERENÇA ENTRE COMUNICAÇÃO SINCRONA E ASSINCRONA E QUAL O MELHOR CENÁRIO PARA UTILIZAR UMA OU OUTRA.
                                <b>
                                    R: Em uma execusao de codigo SINCRONO o codigo executado na sequencia envio e resposta, quando utilizamos ASSINCRONO as chamdas e respostas so são retornadas quando relamente estão prontas aguardando sua execusão.
                                       O melhor cenario par ase utilziar uma chamada assincrona, é quando o metodo precisa da resposta de uma chamada para continuar executando.
                                       Para uma execusão sincrona, quando seu metodo pode continuar executando sem a resposta de determinado metodo.
                                </b>
                                ",
                Contact = new Contact() { Name = "Emerson Galdino", Email = "emersongaldino@hotmail.com", Url = "" }
            });
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                return xmlPath;
            }
        }
    }
}
