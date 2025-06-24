import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
	selector: 'app-error-page',
	imports: [RouterModule],
	templateUrl: './error-page.html',
	styleUrl: './error-page.css',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class ErrorPage implements OnInit {

	constructor(private cdr: ChangeDetectorRef) { }

	ngOnInit(): void {
		this.cdr.detectChanges();
	}
}
