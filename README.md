# Monitoring Web Service

## Запуск
У вас должны быть установлены [зависимости проекта](https://github.com/Bogdan224/MonitoringWebService#зависимости):

1. Запуск API

```cd .../MonitoringApi```

```dotnet run```

API работает на портах 5095 и 7269.

2. Схема API

```https://localhost:7269/swagger```


3. Установка зависимостей

```cd .../MonitoringClient```

```npm install```

4. Запуск сайта

```cd .../MonitoringClient```

```ng serve```

Сайт работает на порте 4200

```https://localhost:4200```

<!--зависимости-->
## Зависимости
Эта программа испульзует [Angular CLI](https://github.com/angular/angular-cli) version 15.2.11. и .NET 8
