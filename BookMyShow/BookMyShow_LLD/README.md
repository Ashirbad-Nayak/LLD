# BookMyShow_LLD

## Project Description
BookMyShow_LLD is a low-level design implementation of a movie ticket booking system. This project aims to provide a scalable and efficient solution for booking movie tickets online.

## Features
- List movies By city
- Select Movie
- List Shows by theatres
- Select Show and seats
- Create booking/reservation
- Make Payment
- Lock & release seats based on booking/payment status

## Lock & release seats based on booking/payment status
- 1.if booking created, lock the seats by updating seat status
- 2.if payment completed(success/failure): success >> book the seats by updating seat status, failure>> release the seats by updating seat status

- Another way :
- instead of relying on external source(Payment service calls our webhook) to update seat status
- maintian a expirydate/TTL for each seat user trying to book
- 1.if booking created, lock the seats by updating seat status and a expiry time
- 2.if payment completed(success/failure), success>>book the seats by updating seat status, failure>> release the seats by updating seat status and reset expiry time
- 3. In case of failure(sevrer goes down or anything) payment service could not update the status, based on expiry time, seats are avaialble by default 
- 4.Incase of success(payment happenned but it took long time), and some other user can see the seats and is trying to book the seats,the expiry time will change
- so even if payment succeeds , now before updating booking status and seat status, 
- we will check the expiry time has changed or not.If changed, we need to initate a refund for user1