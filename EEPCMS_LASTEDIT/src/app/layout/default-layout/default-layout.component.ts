import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NgScrollbar } from 'ngx-scrollbar';


import { IconDirective } from '@coreui/icons-angular';
import {
  ContainerComponent,
  INavData,
  ShadowOnScrollDirective,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarFooterComponent,
  SidebarHeaderComponent,
  SidebarNavComponent,
  SidebarToggleDirective,
  SidebarTogglerDirective
} from '@coreui/angular';

import { DefaultFooterComponent, DefaultHeaderComponent } from './';
import { navItems, GetNavItemsByRole } from './_nav';
import { UserService } from 'src/app/service/user.service';
import { User } from 'src/Models/user.model';
import { CommonModule } from '@angular/common';

function isOverflown(element: HTMLElement) {
  return (
    element.scrollHeight > element.clientHeight ||
    element.scrollWidth > element.clientWidth
  );
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
  standalone: true,
  imports: [
    SidebarComponent,
    SidebarHeaderComponent,
    SidebarBrandComponent,
    RouterLink,
    IconDirective,
    NgScrollbar,
    SidebarNavComponent,
    SidebarFooterComponent,
    SidebarToggleDirective,
    SidebarTogglerDirective,
    DefaultHeaderComponent,
    ShadowOnScrollDirective,
    ContainerComponent,
    RouterOutlet,
    DefaultFooterComponent,
    CommonModule
  ]
})
export class DefaultLayoutComponent implements OnInit  {
  user?:User;

   constructor(private userService: UserService){

   }
  public navItems: INavData[] = [];

  onScrollbarUpdate($event: any) {
    // if ($event.verticalUsed) {
    // console.log('verticalUsed', $event.verticalUsed);
    // }
  }
   ngOnInit(): void {
    this.userService.currentUser.subscribe(user=>{
      // console.log(user)
      this.navItems = GetNavItemsByRole(user?.roles[0]?? "");
    })
    
    // this.navItems = GetNavItemsByRole(this.userService.);
    //  this.userService.user()
    //  .subscribe({
    //    next:(response)=>{
    //    this.user=response;
    //   }
    //  })
   }


   currentUser: User | null = null;

  

 
   isLoggedIn(): boolean {
     return this.currentUser !== null;
   }
}
