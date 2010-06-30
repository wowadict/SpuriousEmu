<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head>
<title>Spurious Server</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="container">
	<div id="headers">
		<div id="nav">
			<ul><li><a href="index.html">Home</a></li>&nbsp&nbsp
			    <li><a href="faq.html">FAQ</a></li>
				&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<li><a href="stats.xml">Server Status</a></li>				
			</ul>
			<ul>
				<li><a href="news.html">News</a></li>
				<li><a href="/forums/index.php">Forums</a></li>
				<li><a href="credits.html">Credits</a></li>
			</ul>
		</div>
	</div>
	<div id="content1">
		<div id="text1">
			
			
			<div class="heading"></div>
			<br />
			<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Account Registration Page</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <style type="text/css" media="screen">@import url(secondary_stats.css);</style>
    <!--[if lt IE 7.]>
    <script defer type="text/javascript" src="pngfix.js"></script>
    <![endif]-->
</head>
<body>
    <center>
    <div class="logo"></div>
    <div style="width:300px">
        <form action="<?php echo $_SERVER['PHP_SELF']; ?>" method="POST">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr class="head"><th colspan="2"><div class="heading"><br><br>Spurious Account Registration Page</div><br><br></th></tr>
            <tr>
                <th>Username: </th><td align="center"><input type="text" name="login" size="30" maxlength="16"/></td>
            </tr>
            <tr>
                <th>Password: </th><td align="center"><input type="password" name="password" size="30" maxlength="16"/></td>
            </tr>
            <tr>
                <th>Retype Password: </th><td align="center"><input type="password" name="retypepass" size="30" maxlength="16"/></td>
            </tr>
            <tr>
                <th>E-mail: </th><td align="center"><input type="text" name="email" size="30" maxlength="30"/></td>
            </tr>
				<!--Added by Jerq--Modified by ElderGod--Modified by W@WAdict>
<th>Account Type:</th><td align="center">
<select name="expansion" type="select">
<option value="0">Normal WoW</option>
<option value="1">Burning Crusade</option>
<option selected value="2">Wrath of The Lich King</option>
<option value="3">Cataclysm</option>
</select><br><br>
        <input type="reset" name="reset" value="Reset"/>
        <input type="submit" name="create" value="Create"/></td>
				<!--Added By Jerq--Modified by ElderGod>
        </table>
        
        </form>

		<?php
		include('config.php');
		$query = @mysql_connect($dbhost,$dbuser,$dbpass) or die(mysql_error());
		mysql_query($query);
		mysql_select_db($db) or die (mysql_error());
        $login=$_POST['login'];
        $password=$_POST['password'];
        $email=$_POST['email'];
        $retype_pass=$_POST['retypepass'];
        $expansion=$_POST['expansion'];
        //$retypelen is the alternate of $retype_pass which i will use in the following syntax to check for field lenght (if the field is empty).Same for the others.
        $loginlen=strlen($login);
        $passwordlen=strlen($password);
        $emaillen=strlen($email);
        $retypelen=strlen($retype_pass);
        if ($password!==$retype_pass or $passwordlen!==$retypelen)
			      {
			  	  print '<font color="red" size="2"><b>The two passwords do not match.Please retype passwords..</b></font>';
				  }
				else{		
        IF ($loginlen > 0 && $passwordlen > 0 && $emaillen > 0 && $retypelen > 0) 
        {
		$query = "insert into accounts(account,password,email,last_ip,expansion) VALUES ('".$login."','".$password."','".$email."','".$_SERVER['REMOTE_ADDR']."','".$expansion."')";
		mysql_query($query);
		print '<font color="green" size="2"><b>Account created successfuly!</b></font>';
		}
		else 
		   {
			IF ($loginlen = 0 or $passwordlen = 0 or $emaillen = 0 or $retypelen = 0)
						{
			  	        print '<font color="red" size="2"><b>Please fill in all the                             fields! One or more fields are not filled...</b></font>';
				        };
		   };
		   			};
$result = mysql_query("SELECT * FROM accounts");
$num_rows = mysql_num_rows($result);
print "<br><br><font color='red' size='2'><b><i>There are curently <font size='3' color='blue'>- $num_rows -</font> registered accounts in the server database .\n</i></b></font>"; 
				        
?>

    </div>
    <div class="footer">
    </div>
    </center>
</body>
</html>	
				
				
			
			
			
		</div>
	</div>
	<div id="pagebottom">
		<div id="left">
			<div id="text2">
				<img src="images/cap-community-news.gif" alt="Community News" />
				
				<p><div class="heading2">First Day of Spurious's New CMS, October/6th/2009</div></p>
				
				<p>Today is the day I release this new CMS system dedicated to the <a href="http://spuriousemu.com">SpuriousEmu Emulator</a> This emu proved itself to be the best emulator yet and was successfuly tested and proved its stability.It was made public not too long ago although it has been under development for quite a long time and as far as i can see its worthy to be called the best.</p>
				
				<p>Like all the emus under development, its not perfect...It has a lot of bugs like every other emu but it gets better by the hour.It has skilled developers that take this project very seriously and do their best to provide us with a free means to play World of Warcraft.</p> 
				
				<p>By this I mean I want to say thanks guys for everything and to encourage them to keep up the good work.</p>
				
				<p>If you're having problems editing this CMS to your webserver specifications and linkage feel free to contact me on spuriousemu.com <a href="http://spuriousemu.com/index.php?option=com_kunena&Itemid=54">forums</a>.</p>
				
				<p>Your are free to edit this CMS.</p>
				<p>PS: Note that every page in this CMS is editable except the credits page.Its fair to give credits to the people who worked to provide this CMS.</p>
				
			</div>
		</div>
		<div id="right">
			<div class="heading2">QUICK LINKS</div>
			<div id="quicklinks">
				<ul>
					<li><a href="http://www.emupedia.com">EMUPEDIA.COM</a></li>
					<li><a href="http://spuriousemu.com/index.php?option=com_kunena&Itemid=54">SpuriousEmu FORUMS</a></li>
					<li><a href="http://wow.allakhazam.com/">Allakhazam</a></li>
					<li><a href="http://www.thottbot.com">Thottbot</a></li>
					<li><a href="http://eldergods.sytes.net">CMS WEBSITE</a></li>
					</ul>
			</div>
			<div class="heading2">SCREENSHOT OF THE DAY</div>
			<img src="images/screenshot.jpg" alt="Screenshot" /> <br />
			</div>
	</div>
	<div id="footer">
		<a href="index.html">Home</a>   |   
		<a href="faq.html">FAQ</a>  |   
	    <a href="stats.xml">Server Status</a>|				
        <a href="register.php">Register</a>|
		<a href="/forums/index.php">Forums</a>|
		<a href="credits.html">Credits</a>
		
		<p><a href="http://spuriousemu.com/index.php?option=com_kunena&Itemid=54">SpuriousEmu FORUMS</a> <br />
			©2009 <a href="http://eldergods.sytes.net">ElderGod@EMUPEDIA.COM</a>. (Original Author).</p>
	</div>
</div>
</body>
</html>