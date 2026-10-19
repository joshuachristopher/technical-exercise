import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface BaseResponse {
  success: boolean;
  message: string;
  responseCode: number;
}

export interface PersonAstronaut {
  personId: number;
  name: string;
  currentRank: string;
  currentDutyTitle: string;
  careerStartDate: string | null;
  careerEndDate: string | null;
}

export interface GetPeopleResult extends BaseResponse {
  people: PersonAstronaut[];
}

export interface GetPersonByNameResult extends BaseResponse {
  person: PersonAstronaut | null;
}

export interface CreatePersonResult extends BaseResponse {
  id: number;
}

@Injectable({
  providedIn: 'root'
})
export class Api {
  private readonly personUrl = '/Person';

  constructor(private readonly http: HttpClient) {}

  getPeople(): Observable<GetPeopleResult> {
    return this.http.get<GetPeopleResult>(this.personUrl);
  }

  getPersonByName(name: string): Observable<GetPersonByNameResult> {
    return this.http.get<GetPersonByNameResult>(
      `${this.personUrl}/${encodeURIComponent(name)}`
    );
  }

  createPerson(name: string): Observable<CreatePersonResult> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<CreatePersonResult>(
      this.personUrl,
      JSON.stringify(name),
      { headers }
    );
  }
}
