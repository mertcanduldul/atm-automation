
# ATM Işlem Yönetimi

##### Bu proje temel anlamda atm otomasyonu için temel business ve otomasyonalar içerir. #####
##### EntityFramework Core kullanılmıştır, kullanıcılar için Basic Auth eklenmiştir. Yatırılan veya değişiklik olan paralar Dakikada bir sisteme otomatik olarak tanıtılmaktadır. #####
##### Uygulama Dockerize edilmiştir. #####


## Kullanılan Teknolojiler

 #### .Net Core 6, Entity.Framework.Core, Docker, Hangfire ####
 
  
## Ekran Görüntüleri

## Swagger ##

![Swagger](https://user-images.githubusercontent.com/32902525/200206159-d6dda456-8691-4a24-88e4-9f0afd3a9305.png)

## Hangfire ##

![Hangfire](https://user-images.githubusercontent.com/32902525/200206208-ad9bf73d-56ad-4c5a-9ea7-a85437d042fc.png)

  
## API Kullanımı

#### Kontrol amaçlı base Metot

```http
  GET /api/Main/Get
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `username` | `string` | **Gerekli**. Basic AuthUsername |
| `password` | `string` | **Gerekli**. Basic AuthPassword|

#### Para Çekme işlemi

```http
  POST /api/Main/WithDraw
```

|Type |Parametre | Tip     | Açıklama                       |
|:--- |:-------- | :------- | :-------------------------------- |
|Header |`username` | `string` | **Gerekli**. Basic AuthUsername |
|Header |`password` | `string` | **Gerekli**. Basic AuthPassword|
|Body |`Money`      | `int` | **Gerekli**. Yatırılmak istenen miktar |
|Body |`MoneyType`      | `enumMoneyType` | **Gerekli**. Yatırılmak istenen para birimi |

##### enumMoneyType = { TRY = 1, USD = 2, EUR = 3 }


#### Para Yatırma işlemi

```http
  POST /api/Main/Deposit
```

|Type |Parametre | Tip     | Açıklama                       |
|:--- |:-------- | :------- | :-------------------------------- |
|Header |`username` | `string` | **Gerekli**. Basic AuthUsername |
|Header |`password` | `string` | **Gerekli**. Basic AuthPassword|
|Body |`Money`      | `int` | **Gerekli**. Yatırılmak istenen miktar |
|Body |`MoneyType`      | `enumMoneyType` | **Gerekli**. Yatırılmak istenen para birimi |

##### enumMoneyType = { TRY = 1, USD = 2, EUR = 3 }

#### Atmde Bulunan Toplam Para

```http
  GET /api/Main/GetTotalMoney
```

|Type |Parametre | Tip     | Açıklama                |
|:--- |:-------- | :------- | :------------------------- |
|Header |`username` | `string` | **Gerekli**. Basic AuthUsername |
|Header |`password` | `string` | **Gerekli**. Basic AuthPassword|

 

  
## Yükleme 

Proje dockerize olduğundan docker-compose ile doğrudan kullanılabilir.

```bash 
cd atm-automation

docker compose up -d
```
    