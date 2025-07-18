import {
	HttpErrorResponse,
	HttpEvent,
	HttpHandler,
	HttpInterceptor,
	HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import Swal from 'sweetalert2';

/**
 * @deprecated
 */
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

	constructor() { }

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		return next.handle(request).pipe(
			catchError((error: HttpErrorResponse) => {
				console.error('Error:',  error);

				Swal.fire({
					icon: 'error',
					title: 'Atenção',
					html: 'Atenção, erro não esperado.<br/>Entre em contato com a equipe de desenvolvimento!',
					showConfirmButton: false,
				});

				return throwError(() => error);
			})
		);
	}
}
