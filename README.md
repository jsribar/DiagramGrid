# DiagramGrid

Zahtjevi na kontrolu za prikaz funkcija:
1.	Treba biti moguće zadati i mijenjati raspon vrijednosti koji se prikazuje. Na primjer x = [-0.5, 3.5], y = [0.0, 14.0]. Granice ne moraju biti cijeli brojevi!
2.	Trebalo bi moći mijenjati veličinu mreže (resize).
3.	Kada se zada raspon vrijednosti, kontrola treba znati iscrtati mrežu tako da su linije na mreži jednoliko razmaknute, na višekratnicima tzv. jediničnog intervala. Jedinični interval se treba izračunati automatski tako da linije na mreži ne budu niti preblizu niti predaleko. Na primjer, najmanji razmak između linija mreže ne smije biti manji od 10 piksela niti veći od 20 piksela (ove granice nećemo zadati konstantama već napraviti konfigurabilnim). Očito će se taj jedinični interval mijenjati kako se mijenja prikazani raspon vrijednosti i veličina mreže.
4.	Bilo bi zgodno, ako su koordinatne osi unutar dijagrama, da one budu prikazane tamnijom linijom.
5.	Vrijednosti koordinata linija mreže bilo bi praktičnije prikazivati uz rub mreže: x vrijednosti ispod donjeg ruba a y vrijednosti lijevo od lijevog ruba. Pri ispisu treba voditi računa da se vrijednosti ne preklapaju!

U suštini, kontrola ima dva dijela:
1.	Mreža u koju će se iscrtavati graf
2.	Okolni dio (ispod i lijevo) u koji se ispisuju vrijednosti na osima
