import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {User} from '../model/User';


export interface IUser {
  id?: number;
  userName: string;
}

@Injectable({providedIn: 'root'})
export class UserService {
  private urlMail: string = "http://localhost:5000";
  private urlPost: string = "https://localhost:5001";

  constructor(private http: HttpClient) {
  }

  getAll(): Observable<IUser[]> {
    return this.http.get<IUser[]>(`${this.urlMail}/api/user/getall`)
      .pipe(catchError(error => {
        return throwError(error);
      }));
  }

  getById(id: number): Observable<IUser> {
    return this.http.get<IUser>(`${this.urlMail}/api/user/${id}`)
      .pipe(catchError(error => {
        return throwError(error);
      }));
  }

  editUser(user: User): Observable<IUser> {
    return this.http.put<IUser>(`${this.urlPost}/api/user/edit/`, user)
      .pipe(catchError(error => {
        return throwError(error);
      }));
  }


  createUser(user: User): Observable<IUser> {
    return this.http.post<IUser>(`${this.urlPost}/api/user/create/`, user)
      .pipe(catchError(error => {
        return throwError(error);
      }));
  }


  deleteUser(id: number): Observable<IUser> {
    return this.http.delete<IUser>(`${this.urlPost}/api/user/delete/${id}`)
      .pipe(catchError(error => {
        return throwError(error);
      }));
  }
}
