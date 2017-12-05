# Monero Idle Miner v0.1

Monero Idle Miner is a console application that utilizes your system idle time to mine Monero. Out-of-the box it works directly with the xmr-stak miner but advanced users will easily be able to swap this out for any other miner they prefer.

Monero Idle Miner is completely free and we don't take away any of your hashrate (no fees!). Therefore my hope is to get this project to the point where it is fully community driven.

Backstory:

I am getting increasily more 'bullish' on Monero therefore I utilize all my resources to get those extra Kh/s whenever possible. My problem with this is that I do a lot of work on my main desktop and additional virtual web servers. I don't want the miners to slow me down so I don't run them when I'm working on my computers. But when I don't I have to manually run and close the miners each time I use or don't use them. I take bathroom or launch breaks, I forget.. it's time consuming and it's a chore.. this is why I created this wrapper that does the work for you.

Simply fire up the application and let it run in the background. It will periodically check if your system is idle and will run the miner for you automatically. If you come back to use your computer it will automatically shutdown so you don't suffer any performance issues.

Windows only for now. If there is demand I will create Linux and Mac versions.

How to run the application:

1. Open xmr-stak.exe and setup your xmr miner as you would normally.

  + If you already have your settings file and miner ready somewhere else just copy all of the files in this directory.
  
2. Fire up IdleMiner.exe and let it do the rest. Stop using your mouse or keyboard for 30 seconds to see it in action.

3. If you want to change the idle time or miner name open idleMiner_settings.txt.
  - You can change how fast you want the miner to launch (your idle time - default to 30 seconds)
  - The second line represent the name of the miner (default xmr-stak.exe)


Donations address: 477eueNFFttThg9BRrGQqjAicWzX45trsYXS3PMv1wfPJ5ct2YvsPTcBNww815hpkxRjs78acfx4mA9HSo95cC1SQf4LHjh
