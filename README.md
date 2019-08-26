# Project - "3dFabrica"

## Type - Retail Store for 3d props

## Description

 - Guest:
-; can see the index page.
-; can register and login.
-; 				
 - User-BUYER:
-; have to register a credits account.
-; should load cash into the account.
-; then can buy products.
-; each purchase gives you points. // fresh users received 30 start points
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
  - Full Name (string)
  - Username (string)
  - Email (string)
  - Gender (enum) (Male/Female)
  - Password (string)
  - ICollection<Order> Orders
  - ICollection<Prop> CreatedProps 
  - ICollection<MarvelousProp> MarvelousProps       
  - CreditAccountId (string)
  - CreditAccount (virtual CreditAccount)

  
### Prop
  - Id (string)
  - Name (string)
  - Type (enum) (Pants/Shorts/Tights/Skirt/Dress/ShortSleeve/Longsleeve/Tank/Hoodie/Jacket/Vest/Bra/Equipment)
  - Price (double)
  - ImageUrl (string)
  - Hashtags (string)
  - Description (string)
  - IsDeleted (bool)
  - PropCreatorId (string)
  - PropCreator (FabricaUser)
  - ICollection<PropOrder> Orders

### MarvelousProp
  - Id (string)
  - Name (string)
  - Type (enum) (Magicians/Astronauts/Divers/Holiday)
  - Points (int)
  - ImageUrl (string)
  - Hashtags (string)
  - Description (string)
  - IsDeleted (bool)
  - MarvelousCreatorId (string
  - MarvelousCreator (FabricaUser)
  - ICollection<MarvelousPropOrder> Orders 
  
### Order
  - Id (string)
  - ClientId (string)
  - Client (FabricaUser)
  - IsDeleted (bool)
  - IsActive (bool)
  - OrderedOn (DateTime)
  - ICollection<MarvelousPropOrder> MarvelousProps 
  - ICollection<PropOrder> Props


### CreditAccount
  - AccountId (string)
  - CardNumber (string)
  - Points (int)
  - Cash (double)
  - AccountOwnerId (string)
  - AccountOwner (virtual FabricaUser)

### Licenze
  - LicenzeId (string)
  - Name (string)
  - Type (LicenzeType) 
  - Price (double)
  - bonusPoints (int)
  
## MoreAccurateDescription :
### User
	- first time logged user. //can see every prop after login. He receives 30 startpoints
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

	- The licence gives points to a user that he can use to buy only "Marvelous collections" of props.
	
### 3D Prop(product) 
	- get full cash price from buyer account
	- gives a points to a users who bought it. 
		formula :
		pointhalf = (cashprice/2) 
		bonuspoints = ((cashprice/2) + 1 * 0.50) 
		points = pointshalf + bonuspoints
	- add a cash (price * 0.70) - 70% to a user who sell this product. 
	- add a cash (price * 0.30) - 30% to the Admin account. 
	- if logged use is admin then the price is 90% off

### Marvelous collection of props
	- get a full range of points from buyer account
	- gives back 300 points to a buyer account
	- if logged use is admin then the price is 90% off
	- those collection will be in range of points(price) 500 - 2500.
	- users can pay for "Marvelous collection" with collected points.
	- if the user want to get some collection ,but doesn't have enough points he can buy a license.
	- if user collect 5 "Marvelous collection",will unlock option in his profile to create a collections.Then if someone takes some of his collections ,he will get 300 points for each order.

### Order
	- user can add products to basket
	- user can cancel those products
	- when confirm order(all products from order at once) with a confirm button 
	- transaction to admin,creator and buyer would be made
	- generate an order information for all confirmed and unconfirmed products and orders
