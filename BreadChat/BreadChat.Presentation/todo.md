﻿### TODO 06 11 2024
- [x] Add pagination GET 
  - [x] /api/users
  - [x] /api/channels (not tested)
  - [x] /api/channels/{channelId}/messages (the pages start at 0)
- [x] Add patch (update) endpoints for
  - [x] /api/users/{userId}
- [x] Add swagger documentation for all endpoints (ask to check correctness especially the Membership one)
- [x] Add author property to message
- [x] Add channel membership controller
  - [x] [GET] /api/channels/{channelId}/members -- Get all members of a channel
  - [x] [POST] /api/channels/{channelId}/members -- Add a member to a channel
  - [x] [DELETE] /api/channels/{channelId}/members/{userId} -- Remove a member from a channel

### BUGS
- Users can post messages in channels which they are not members of