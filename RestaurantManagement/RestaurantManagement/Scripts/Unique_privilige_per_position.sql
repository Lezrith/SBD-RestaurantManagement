﻿ALTER TABLE Having_privileges
ADD CONSTRAINT Unique_privilege_per_position UNIQUE (Privilege_Name, Position_Name)