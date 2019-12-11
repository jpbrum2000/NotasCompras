import { Component, OnInit, TemplateRef } from '@angular/core';
import { NotaCompra } from 'src/app/model/NotaCompra';
import { FormGroup, FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal/';

@Component({
  selector: 'app-aprovacao-notas-compra',
  templateUrl: './aprovacao-notas-compra.component.html',
  styleUrls: ['./aprovacao-notas-compra.component.css']
})
export class AprovacaoNotasCompraComponent implements OnInit {
  formPesquisa: FormGroup;
  modalRef: BsModalRef;

  listNotasCompra: NotaCompra[] = null;
  dataInicio: Date;
  dataFim: Date;
  userId: string;


  constructor(
    private fb: FormBuilder,       
    private modalService: BsModalService,  
    private authService: AuthService,
    private  httpClient: HttpClient
  ){}

  ngOnInit() {
    this.formPesquisa = this.fb.group({     
      dataInicio: ['2019-12-01'],
      dataFim: ['2019-12-31']
    });
  }

  onFiltrar() {
    this.httpClient.post<NotaCompra>("http://localhost:5000/PesquisaNotaCompraAprovacao",
    {
      "dataInicio":  this.formPesquisa.value.dataInicio,
      "dataFim": this.formPesquisa.value.dataFim,
      "usuarioId": this.authService.getUser().id
    })
    .subscribe(
      data  => {
        let response: any = data;
        this.listNotasCompra = response;
        console.log(this.listNotasCompra.length);
      },
      error  => {
        console.log("Error", error);
      }
    );
  }
  selectedNotaCompraValidarId: string;
  openModal(template: TemplateRef<any>,_selectedNotaCompraValidarId) {
    this.selectedNotaCompraValidarId = _selectedNotaCompraValidarId;
    this.modalRef = this.modalService.show(template);
  }
  confirm(): void {
    this.httpClient.post("http://localhost:5000/AprovacaoNotaCompra",
    {
      "idNotaCompra":  this.selectedNotaCompraValidarId,
      "usuarioId": this.authService.getUser().id
    })
    .subscribe(
      data  => {
        alert('Visto/Aprovacao registrado com sucesso!');
        this.onFiltrar();
      },
      error  => {
        alert('Erro ao registrar Visto/Aprovacao!');
        console.log("Error", error);
      }
    );
    this.modalRef.hide();
  }
 
  decline(): void {
    this.modalRef.hide();
  }

}
