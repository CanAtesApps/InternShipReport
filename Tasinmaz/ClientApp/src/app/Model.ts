export class Model
{
  Tasinmazlar: Array<Tasinmaz>;
}

export class Tasinmaz{
  tasinmazID ;
  cityName;
  countyName;
  areaName;
  ada;
  parsel;
  nitelik;
  adres;
  isActive;
}
export class TasinmazPost{
  cityID;
  countyID;
  areaID;
  ada;
  parsel;
  nitelik;
  adres;
  isActive;
  /**
   *
   */
  constructor() {
  }
}

export class KullaniciTable
{
  userID;
  ad;
  soyad;
  email;
  rol;
}

export class TasinmazPut{
  tasinmazID;
  cityID;
  countyID;
  MahalleID;
  ada;
  parsel;
  nitelik;
  adres;
  isActive;
  /**
   *
   */
  constructor() {
  }
}
export class Helper{
  static selectedTasinmaz : Tasinmaz;
  static selectedTasinmazPost : TasinmazPost
}

export class Il{
  cityID;
  cityName;
}

export class Ilce{
  countyID;
  countyName;
  cityID;
}

export class Mahalle{
  mahalleID;
  mahalleName;
  countyID;
}

export class pageInfo{
 maxPageNumber;
}

export class userPost{
  ad;
  soyad;
  email;
  password;
  rol;

  constructor() {
  }
}
export class UserAlldetails{

  userID;
  ad;
  soyad;
  email;
  rol;
  password;
  isActive;

  constructor() {

  }
}

export class Log{
  logID;
  logDetails;
  logTime;
  logSuccess;
  ipAdress;
}

