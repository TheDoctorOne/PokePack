package me.mahmutkocas.amialive;

import net.minecraftforge.fml.common.eventhandler.SubscribeEvent;
import net.minecraftforge.fml.common.gameevent.TickEvent.ClientTickEvent;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.SocketException;

public class AliveChecker {

    public static DatagramSocket socket;
    private static long latestPacketTime = -1;

    private static long tickCounter = 0;
    @SubscribeEvent
    public static void clientTic(ClientTickEvent event) {
        if(latestPacketTime == -1)
            latestPacketTime = tickCounter;
        if (tickCounter%15==0) {

                if (!socket.isClosed()) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            try {
                            byte[] buffer = new byte[5];
                            DatagramPacket req = new DatagramPacket(buffer, buffer.length);
                            try {
                                socket.receive(req);
                            } catch (SocketException ignored) {
                            }
                            StringBuilder s = new StringBuilder();
                            for (byte b : buffer) {
                                char c = (char) b;
                                s.append(c);
                            }
                            if (s.toString().equals(References.packet))
                                latestPacketTime = tickCounter;
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }).start();
                }

        }
        int timeout = 20 * 4;
        if(latestPacketTime + timeout < tickCounter)
            System.exit(0);

        tickCounter++;
    }

}
