import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { NotificationComponent } from './components/modals/notification/notification.component';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  bsModalRef:BsModalRef;
  constructor(private modalServices:BsModalService) { }

  showNotifaction(isSuccess:boolean ,title:string ,message:string){

    const initState :ModalOptions={
       
      initialState:{
        isSuccess,title,message
      }
    };

    this.bsModalRef = this.modalServices.show(NotificationComponent,initState);
  }

}
