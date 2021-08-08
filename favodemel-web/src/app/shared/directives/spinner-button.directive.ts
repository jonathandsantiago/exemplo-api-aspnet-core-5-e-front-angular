import {Directive, ElementRef, Input, OnInit, Renderer2} from '@angular/core';

@Directive({
  selector: '[appSpinner]'
})
export class SpinnerButtonDirective implements OnInit {
  @Input('appSpinner') set isLoading(loading) {
    if (!this.spinner) {
      return;
    }

    if (loading) {
      this.show();
    } else {
      this.hide();
    }
  }

  spinner: any;

  constructor(protected elementRef: ElementRef,
              protected renderer: Renderer2) {
  }

  ngOnInit() {
    this.spinner = this.renderer.createElement('div');
    this.renderer.addClass(this.spinner, 'spinner-border');
    this.renderer.addClass(this.spinner, 'spinner-button');
  }

  show() {
    this.renderer.appendChild(this.elementRef.nativeElement.firstChild, this.spinner);
  }

  hide() {
    this.renderer.removeChild(this.elementRef.nativeElement.firstChild, this.spinner);
  }
}
