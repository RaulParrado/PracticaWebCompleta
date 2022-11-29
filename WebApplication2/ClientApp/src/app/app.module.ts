import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ContactoComponent } from './contacto/contacto.component';
import { PokemonsComponent } from './pokemons/pokemons.component';
import { AttackComponent } from './attacks/attacks.component';
import { EditAttackComponent } from './attacks/edit/editAttack.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ContactoComponent,
    PokemonsComponent,
    AttackComponent,
    EditAttackComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'contacto', component: ContactoComponent },
      { path: 'pokemons', component: PokemonsComponent },
      { path: 'pokemons/:id', component: AttackComponent},
      { path: 'pokemons/:id/edit/:attackId', component: EditAttackComponent },
      { path: 'pokemons/:id/new', component: EditAttackComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
