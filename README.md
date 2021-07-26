# LeaderBoardTracker
Leader Board Tracker Web API

There are 2 tables form which you can access/modify data from. 
1) Player
2) LeaderBoard

The URLs are 
1) api/players
2) api/leaderboards

There are 4 methods GET, POST, PUT and DELETE.

For Player--
In GET, you can access data by 3 types

1) get all entries - api/players
2) get by id - api/players/{id}/player
3) get by name - api/players/byname?fn="a"&ln="b"

In POST, you can add a player. The input json will look like 
{
        "firstname": "John",
        "lastname": "Smith",
        "email": "john.smith@gmail.com"
}

Here, all the **3** attributes are **REQUIRED**.

The PUT method follows the same structure too. All the attributes are required here too.

The URL is same for both add and update method
api/players

It differs only in the method
The output for get all players is a list.
For example - 
[
    {
        "id": 1,
        "firstName": "Olivia",
        "lastName": "Jones",
        "email": "olivia.jones@gmail.com",
        "operation": "Get Player "
    },
    {
        "id": 2,
        "firstName": "Jane",
        "lastName": "Doe",
        "email": "Jane.doe@gmail.com",
        "operation": "Get Player "
]
The output for getting a player by id and name --
{
    "id": 1,
    "firstName": "Olivia",
    "lastName": "Jones",
    "email": "olivia.jones@gmail.com",
    "operation": "Get Player ",
    "statusCode": 0,
    "errorMessage": null
}
The output for adding or updating a player --
{
    "id": 1,
    "firstName": "Olivia",
    "lastName": "Jones",
    "email": "olivia.jones@gmail.com",
    "operation": "Add Player ",
    "statusCode": 200,
    "errorMessage": []
}
The out for deleting --
{
    "id": 1,
    "operation": "Remove Player ",
    "statusCode": 200,
    "errorMessage": []
}

For the DELETE method, you can delete a player by just using the id
api/players/1
But the method must be DELETE

For LeaderBoard --
In GET, you can access the by 3 types

1) get all entries - api/leaderboards
2) get by id - api/leaderboards/1
3) get by playerid - api/leaderboards/playerid/1

For get all entries, you can order by the columns TotalScore or GamesPlayed. It can be ordered in ascending or descending order.
For example -
api/leaderboards?orderby="gamesplayed"&orderbykey="asc"
api/leaderboards?orderby="totalscore"&orderbykey="desc"

In POST, you can add an entry for a player. The input json will look like 
[
  {
        "id": 1,
        "gamesplayed": 10,
        "totalscore": 10
  },
  {
        "id": 2,
        "gamesplayed": 5,
        "totalscore": 5
  }
]
Here, we can send multiple entries for POST, PUT and DELETE methods.
Also, **id**,**gamesplayed** is **REQUIRED**.

The output for getting all the entries in the leader board --
[
    {
        "playerId": 2,
        "gamesPlayed": 3,
        "totalScore": 3,
        "operation": "Get Players from Leader Board ",
        "statusCode": 0,
        "errorMessage": null
    },
    {
        "playerId": 6,
        "gamesPlayed": 5,
        "totalScore": -5,
        "operation": "Get Players from Leader Board ",
        "statusCode": 0,
        "errorMessage": null
    }
]

output for getting entry by id and playerid
{
    "playerId": 2,
    "gamesPlayed": 3,
    "totalScore": 3,
    "operation": "Get Player from Leader Board with PlayerId ",
    "statusCode": 0,
    "errorMessage": null
}

output for adding and updating an entry
[
    {
        "playerId": 5,
        "gamesPlayed": 5,
        "totalScore": 5,
        "operation": "Add Players to Leader Board ",
        "statusCode": 200,
        "errorMessage": null
    }
]

output for deleting an entry
[
    {
        "playerId": 5,
        "firstName": "Poppy",
        "lastName": "Jones",
        "operation": "Remove Players from Leader Board ",
        "statusCode": 200,
        "errorMessage": null
    }
]
