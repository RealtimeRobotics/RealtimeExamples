﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_commander.Responses
{
    public enum ResponseCode
    {
        UNRECOGNIZED_RESPONSE_CODE = -1,
        SUCCESS = 0,

        RTR_SERVER_ERROR = 1001,
        ROBOT_STATE_ERROR = 1002,
        CONNECTION_ERROR = 1003,
        FAILED_TO_CLEAR_FAULTS = 1004,
        MPA_ERROR = 1005,

        SERVER_NOT_IN_OPERATION_MODE = 2001,
        CANCELED_BY_USER = 2002,
        ARGUMENTS_INVALID = 2003,
        SERVER_NOT_IN_CONFIG_MODE = 2004,
        RAPID_SENSE_LOAD_ERROR = 2005,
        BEGIN_OPERATION_MODE_TIMEOUT = 2006,
        CANNOT_CALL_OFFROAD_WHILE_MOVING = 2007,
        RAPID_SENSE_NOT_IN_RUN_MODE = 2008,
        CHANGE_WORK_STATE_INVALID_WHILE_IN_MOTION = 2009,
        PROJECT_ALREADY_EXISTS = 2010,
        PROJECT_IN_HANDOFF_MODE = 2011,
        PROJECT_NOT_IN_HANDOFF_MODE = 2012,

        DECONFLICTION_GROUP_NOT_FOUND = 3001,
        PROJECT_NOT_FOUND = 3002,
        WORK_STATE_NOT_FOUND = 3003,
        PROJECT_LOAD_IN_PROGRESS = 3004,
        PROJECT_CANNOT_RUN_SIMULATED_HW = 3005,
        DECONF_GROUP_HAS_NO_PROJECTS = 3006,
        VOXEL_REGION_NOT_VERIFIED = 3007,
        OTHER_DECONFLICTION_GROUP_ALREADY_LOADED = 3008,
        DECONF_GROUP_NOT_LOADED = 3009,

        NO_COLLISION_FREE_PATH = 4001,
        NO_CONFIG_WITHIN_TOL_TO_GOAL = 4002,
        NO_IK_SOLUTION_WITHIN_TOL_TO_ROADMAP = 4003,
        HUB_NOT_IN_ROADMAP = 4004,
        START_CONFIG_NOT_WITHIN_TOL_TO_ROADMAP = 4005,
        REPLAN_ATTEMPTS_EXCEEDED = 4006,
        ALREADY_EXECUTING_A_GOAL = 4007,
        OG_READ_FAILURE = 4008,
        PATH_PLANNING_TIMEOUT = 4009,
        INVALID_START_END_INDEX = 4010,
        CAMERA_ERROR = 5001,
        COMMAND_CONFLICT = 7001,
        TRAJECTORY_INVALID_IK = 7002
    }
}