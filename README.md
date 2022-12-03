# WordDisplayer
Tool to display words.  

**In the "Configuration" folder in the root of the .exe, you can add .txt with all the words you would like;

**You can also add a comma after the word and specify the image that goes along with the word.(optional)
All images must be added in the "Images" folder in the root of the .exe;

**You can also parse your file with the red button on te top left of the screen.
This will take every word in your file and put each one on its own line;

***CAUTION: if associated an image with a world using ',', parsing the file will undo this add the image on a new line.***
*** Adding the #DONT_PARSE keyword on the first line of a file blocks it from being parsed. ***

***Example of file***
#DONT_PARSE
cloud, cloud.png
sun, sun.png
rainbow
sky
sea, sea.png

***Example of parsing***
-File Before:
word     jjd 78098.0l ji 09
thing
cloud computer ddd

-File After:
word
jjd
ji
thing
cloud
computer
ddd
