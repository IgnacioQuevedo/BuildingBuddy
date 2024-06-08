import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MaintenanceCreateResponse } from '../Interfaces/maintenance-create-response';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { MaintenanceCreateRequest } from '../Interfaces/maintenance-create-request';
import { MaintenanceRequest } from '../Interfaces/maintenanceRequest.model';
import { ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MaintenanceRequestService {

  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  createMaintenanceRequest(maintenanceToCreate: MaintenanceCreateRequest): Observable<MaintenanceCreateResponse> {
      return this.http.post<MaintenanceCreateResponse>(`${environment.apiBaseUrl}/api/v2/maintenance`, maintenanceToCreate);
  }

  getAllMaintenanceRequests(managerId: string): Observable<MaintenanceRequest[]> {
    const params = new HttpParams()
    .set('managerId', managerId)
    return this.http.get<MaintenanceRequest[]>(`${environment.apiBaseUrl}/api/v2/maintenance/requests?`, {params});
  }

  getMaintenanceRequestById(maintenanceRequestId: string): Observable<MaintenanceRequest> {
    return this.http.get<MaintenanceRequest>(`${environment.apiBaseUrl}/api/v2/maintenance/requests/${maintenanceRequestId}`);
  }

  assignMaintenanceRequest(maintenanceRequestIdFromParam: string, requestHandlerIdFromParam: string): Observable<MaintenanceRequest> {
    const params = new HttpParams()
    .set('idOfRequestToUpdate', maintenanceRequestIdFromParam)
    .set('idOfWorker', requestHandlerIdFromParam);

    const url = `${environment.apiBaseUrl}/api/v2/maintenance/request-handler/requests`;
    
    return this.http.put<MaintenanceRequest>(url, {}, { params });
  }

}
