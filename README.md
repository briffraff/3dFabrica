# Project - 3dFabrica

## Type - Retail Store

## Description

 - Guest:
-; can see the index page.
-; can register and login.
-; 				
 - User-BUYER:
-; have to register a credits account.
-; should load cash into the account.
-; then can buy products.
-; each purchase gives you points. // fresh users received 10 start points
-; can use collected points to get a "Marvelous collections".
-; in case of insufficient availability of points can buy a licence.
-; can create a props for selling.
-; if bought 10 props then will receive 100 points into the account.
-; if collect 5 "Marvelous collections" will be able to create them.
-; 
 - User-SELLER:
-; have to register a credit account.
-; is not required to put cash into the account,but in this case can't buy a products or licences.
-; started with 10 points,but can't collect more points ,because of the zero cashflow.
-; can't get any "Marvelous collections".
-; can create a props for selling.
-; if sell 10 props then will receive 100 points into the account.
-; 
 - Admin:
-; can manage all registered users.
-; can manage all the props and collections.
-; can create "Marvelous collections".

## Entities

### User
  - Id (string)
  - Username (string)
  - Password (string)
  - Email (string)
  - Full Name (string)
  
### 3DProp
  - Id (string)
  - Name (string)
  - Type (enum) (Automotive/Home/Fabrics etc . . .)
  - Price (decimal)

### MarvelousProp
  - Id (string)
  - Name (string)
  - PointsPrice (int)
  - MarvelousOwner (User)
  
### Order
  - Id (string)
  - IssuedOn (dateTime)
  - Quantity (int)
  - 3DProp (3DProp)
  - MarvelousProp (MarvelousProp)
  - Issuer (User)

### CreditAccount
  - CardNumber (string)
  - Active Since (dateTime)
  - Points (int)
  - Cash (decimal)
  - AccountOwner (User)
  - Licenses (list of License)

### License
  - Id (string)
  - IssuedOn (dateTime)
  - Type (enum) (Basic/Advanced/Expert/Proffesional)
  - CreditAccount creditAccount
  - Order (Order)
  - LicenseOwner (User)
  
## MoreAccurateDescription :
### User
	- first time logged user. //can see every prop after login. He receives 10 startpoints
	- registered users can sell products. //if having a credit account registered
	- registered users can buy products. //if having a credit account registered and cash deposit in it
	- registered users can get a "Marvelous Collection". //if having enough points.

### Credit account
	- register a credit card. //(required if a user wants to buy/sell)
	- load а cash deposit. //user needs a cash if he want to buy props.
	- collecting points of purchased props.
	- user doesn't need to load a cash deposit,if he want just to selling props.
	- user is able to cashout in everytime.
	
### License
	- need to have a credit account registered. //credit card and cash deposit
	- when purchase a licence :
		-- basic licence - cost 30 cash ,receive 75 points in your credit account.
		-- advanced licence - cost 100 cash, receive 250 points.
		-- expert licence - cost 500 cash , receive 1250 points.
		-- proffesional licence - cost 2000 cash.
			--- this licence gives you access to each prop or "Marvelous collection" for a year.
	- The licence gives points to a user that he can use to buy only "Marvelous collections" of props.
	
### 3D Prop(product) 
	- gives a points(+3) to a users who bought it. //the buyer win 3 point on each purchase
	- add a cash (propPrice/2) to a user who sell this product. //half of the price goes to the seller
	- add a cash (propPrice/2) to the Admin account. //half of the price goes to the site owner
	
### Marvelous collection of props
	- those collection will be in range of points(price) 500 - 2500.
	- users can pay for "Marvelous collection" with collected points.
	- if the user want to get some collection ,but doesn't have enough points he can buy a license.
	- if user collect 5 "Marvelous collection",will unlock option in his profile to create a collections.Then if someone takes some of his collections ,he will get 300 points for each order.

### Order
	- generate an order information for all orders