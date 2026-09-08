import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowersPage } from './followers-page';

describe('FollowersPage', () => {
  let component: FollowersPage;
  let fixture: ComponentFixture<FollowersPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FollowersPage],
    }).compileComponents();

    fixture = TestBed.createComponent(FollowersPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
