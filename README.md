# BackgroundProcAPI

Exemplo de API em .NET 8 contendo endpoint para processamento em background.

### Instruções:

1. Execute o endpoint e obtenha o idBackgroundProc: https://localhost:44341/WeatherForecast/GetWeatherForecastBackground

2. Execute o endpoint como o id obtido: https://localhost:44341/WeatherForecast/GetStatus/idBackgroundProc

- 2.1. Dentro de 70.000ms, o endpoint retornará: "Processing in progress..."

- 2.2. Após 70.000ms, o endpoint deve retornar: "Processing completed successfully."

- 2.3. Note que na saída, via LogInformation, teremos a mensagem "Background execution completed."
