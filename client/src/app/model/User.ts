import {IUser} from "../user/user.service";

export class User implements IUser{
  id?: number;
  userName: string;
  constructor(id: number, userName: string){
    this.id = id;
    this.userName = userName;
  }
}
