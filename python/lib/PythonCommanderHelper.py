#!/usr/bin/env python3

import requests

class ApiError(Exception):
    """An API Error Exception"""
    def __init__(self, status):
        self.status = statu
    def __str__(self):
        return "APIError: status={}".format(self.status)

class PythonCommanderHelper(object):
    groups = '/api/groups/'
    installed_proj = '/api/projects/'
    proj_details = '/api/projects/:project/'
    load_group = '/api/groups/load/:group'
    unload_group = '/api/groups/unload/:group'
    config_mode = '/api/appliance/mode/config/'
    clear_faults = '/api/appliance/clear_faults/'
    teleport_robot = '/api/projects/:project/hubs/:hub/'
    
    def __init__(self,ip_adr):
        self.ip_adr = ip_adr

    def send_get_request(self,extension):
        url = 'http://%s%s'%(self.ip_adr,extension)
        resp = requests.get(url)
        print('\n[INFO] Sent Get request to %s'%(url))

        if resp.status_code != 200:
            # This means something went wrong.
            raise ApiError('GET {} {}'.format(url,resp.status_code))
        
        return resp.json()

    def send_put_request(self,extension):
        url = 'http://%s%s'%(self.ip_adr,extension)
        resp = requests.put(url)
        print('\n[INFO] Sent Put request to %s'%(url))

        if resp.status_code != 200:
            # This means something went wrong.
            raise ApiError('PUT {} {}'.format(url,resp.status_code))

    def get_group_info(self):
        groups = self.send_get_request(self.groups)
        # print(groups)
        group_info = {}
        for group in groups:
            group_name = group['name']
            group_projects = group['projects']
            loaded = group['loaded']
            group_info.update({group_name : {'loaded':loaded,'projects':group_projects}})
        return group_info

    def get_installed_projects(self):
        projs = self.send_get_request(self.installed_proj)
        print(projs['projects'])
        return projs

    def put_load_group(self,group_name):
        extension,place_holer = self.load_group.split(':')
        extension = extension + group_name
        resp = self.send_put_request(extension)

        return resp
    
    def put_unload_group(self,group_name):
        extension,place_holer = self.unload_group.split(':')
        extension = extension + group_name
        resp = self.send_put_request(extension)

        return resp

    def get_project_info(self,projects):
        project_info = {}
        for project in projects:
            # Format string for project info get request
            extension,place_holer = self.proj_details.split(':')
            extension = extension + project + '/'
            resp = self.send_get_request(extension)

            workstates = resp['roadmaps']
            hubs = resp['hubs']
            project_info.update({project:{'workstates':workstates,'hubs':hubs}})
            
        return project_info
    
    def put_config_mode(self):
        resp = self.send_put_request(self.config_mode)
        return resp
    
    def put_teleport_robot(self,project,hub):
        split_string = self.teleport_robot.split(':')
        prefix = split_string[0]
        project = '%s/hubs/'%(project)
        hub = '%s'%(hub)

        teleport_string = prefix + project + hub
        resp = self.send_put_request(teleport_string)

        return resp

    def get_hubs(self):
        pass

