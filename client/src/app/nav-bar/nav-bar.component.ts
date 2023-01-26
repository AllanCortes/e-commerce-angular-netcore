import { Component, OnInit } from '@angular/core';
import { faCartShopping,faBars} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  constructor() { }
  ngOnInit() {

  }
  faCartShopping = faCartShopping;
  faBars = faBars;
}
