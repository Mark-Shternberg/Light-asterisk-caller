# Light-asterisk-caller
<center><b>(!) - this program isn't a softphone, read how call work below - (!)</b><br><br></center>
This program can:
<li>Listen for incoming calls (message bout call will appear at the bottom right of the monitor)
<li>Take photo, name, job title and description of person from LDAP
<li>Take name from Asterisk
<li>Make calls (also by choosing person to call from LDAP)
<li>Be a default call program (for tel links)

<br><br>
Screenshots:
<br><br>

![Screenshot of Log View Web UI](https://medvedev-it.ru/wp-content/uploads/2023/04/make_call.png)

![Screenshot of Email Show Modal](https://medvedev-it.ru/wp-content/uploads/2023/04/income_call.png)

# Install
Check [Release page](https://github.com/Mark-Shternberg/Light-asterisk-caller/releases) for last release<br>
Download Windows installer via link in release description<br>
After install will appear ini file. Replace example settings by yours, save and close
<br><br>
If you will need to edit settings after install, ini file will be here:
```
%userprofile%\AppData\Roaming\Light-asterisk-caller\
```

# How call works
When you click tel link or call from UI:
1. Program send command to Asterisk
2. Asterisk make call to your phone number
3. When you take phone it starts calling to number from link or UI
