import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainPageSidebar } from '../main-page-sidebar/main-page-sidebar';

@Component({
  selector: 'app-main-page-layout',
  imports: [RouterOutlet, MainPageSidebar],
  templateUrl: './main-page-layout.html',
  styleUrl: './main-page-layout.scss'
})
export class MainPageLayout {

}
