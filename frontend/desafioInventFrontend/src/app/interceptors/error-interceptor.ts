import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import Swal from 'sweetalert2';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
	return next(req).pipe(
		catchError((error: HttpErrorResponse) => {
			console.error('Error:', error);

			Swal.fire({
				icon: 'error',
				title: 'Atenção',
				html: 'Atenção, erro não esperado.<br/>Entre em contato com a equipe de desenvolvimento!',
				showConfirmButton: false,
			});

			return throwError(() => error.error);
		})
	);
};
