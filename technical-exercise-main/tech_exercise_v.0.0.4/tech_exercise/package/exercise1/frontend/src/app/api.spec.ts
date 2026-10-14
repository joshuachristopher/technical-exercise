import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Api, CreatePersonResult, GetPeopleResult, GetPersonByNameResult } from './api';

describe('Api', () => {
  let service: Api;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(Api);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getPeople should GET /Person', () => {
    const mockResponse: GetPeopleResult = {
      success: true,
      message: 'Successful',
      responseCode: 200,
      people: []
    };

    service.getPeople().subscribe((result) => {
      expect(result).toEqual(mockResponse);
    });

    const req = httpMock.expectOne('/Person');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('getPersonByName should GET /Person/{name}', () => {
    const name = 'John Doe';
    const mockResponse: GetPersonByNameResult = {
      success: true,
      message: 'Successful',
      responseCode: 200,
      person: {
        personId: 1,
        name,
        currentRank: '1LT',
        currentDutyTitle: 'Commander',
        careerStartDate: null,
        careerEndDate: null
      }
    };

    service.getPersonByName(name).subscribe((result) => {
      expect(result).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`/Person/${encodeURIComponent(name)}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('createPerson should POST /Person with a JSON string body', () => {
    const name = 'Jane Doe';
    const mockResponse: CreatePersonResult = {
      success: true,
      message: 'Successful',
      responseCode: 200,
      id: 2
    };

    service.createPerson(name).subscribe((result) => {
      expect(result).toEqual(mockResponse);
    });

    const req = httpMock.expectOne('/Person');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toBe(JSON.stringify(name));
    expect(req.request.headers.get('Content-Type')).toBe('application/json');
    req.flush(mockResponse);
  });
});
