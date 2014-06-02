<?php
	// =============================================================================
	// WebFX bootstrapper - loads the application modules and executes WebFX
	// Copyright (C) 2013-2014  Mike Becker
    // 
	// This program is free software: you can redistribute it and/or modify
	// it under the terms of the GNU General Public License as published by
	// the Free Software Foundation, either version 3 of the License, or
	// (at your option) any later version.
    // 
	// This program is distributed in the hope that it will be useful,
	// but WITHOUT ANY WARRANTY; without even the implied warranty of
	// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	// GNU General Public License for more details.
    // 
	// You should have received a copy of the GNU General Public License
	// along with this program.  If not, see <http://www.gnu.org/licenses/>.
	// =============================================================================
	
	// We need to get the root path of the Web site. It's usually something like
	// /var/www/yourdomain.com.
	global $RootPath;
	$RootPath = dirname(__FILE__);
	
	// Now that we have defined the root path, load the WebFX content (which also
	// include_once's the modules and other WebFX-specific stuff)
	require_once("WebFX/WebFX.inc.php");
	require_once("Include/UUID.inc.php");
	
	// Bring in the WebFX\System and WebFX\IncludeFile classes so we can simply refer
	// to them (in this file only) as "System" and "IncludeFile", respectively, from
	// now on
	use WebFX\System;
	use WebFX\IncludeFile;
	
	// Tell WebFX that this is a tenanted hosting application. This will allow us to
	// control much of the application through Tenant Manager rather than having to
	// continually push out code updates.
	System::$EnableTenantedHosting = true;
	
	// Tell WebFX that we are ready to launch the application. This cycles through
	// all of the modules (usually you will define your main application content in
	// 000-Default) and executes the first module page that corresponds to the path
	// the user is GETting.
	System::Launch();
?>