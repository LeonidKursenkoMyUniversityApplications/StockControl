-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: depotdb
-- ------------------------------------------------------
-- Server version	5.5.49

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `destination`
--

DROP TABLE IF EXISTS `destination`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `destination` (
  `id_destination` int(11) NOT NULL,
  `type` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_destination`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `destination`
--

LOCK TABLES `destination` WRITE;
/*!40000 ALTER TABLE `destination` DISABLE KEYS */;
INSERT INTO `destination` VALUES (1,'виробництво'),(2,'замовник');
/*!40000 ALTER TABLE `destination` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fashion`
--

DROP TABLE IF EXISTS `fashion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fashion` (
  `id_fashion` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `definition` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id_fashion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fashion`
--

LOCK TABLES `fashion` WRITE;
/*!40000 ALTER TABLE `fashion` DISABLE KEYS */;
INSERT INTO `fashion` VALUES (1,'класична','має діловий вигляд'),(2,'спортивна','має спортивний вигляд');
/*!40000 ALTER TABLE `fashion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `issued_materials`
--

DROP TABLE IF EXISTS `issued_materials`;
/*!50001 DROP VIEW IF EXISTS `issued_materials`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `issued_materials` AS SELECT 
 1 AS `id_issued_material`,
 1 AS `date`,
 1 AS `amount_material`,
 1 AS `id_material`,
 1 AS `id_destination`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `issuedview`
--

DROP TABLE IF EXISTS `issuedview`;
/*!50001 DROP VIEW IF EXISTS `issuedview`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `issuedview` AS SELECT 
 1 AS `id_issued_material`,
 1 AS `date`,
 1 AS `amount_material`,
 1 AS `name`,
 1 AS `type`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `manufacturer`
--

DROP TABLE IF EXISTS `manufacturer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `manufacturer` (
  `id_manufacturer` int(11) NOT NULL,
  `name` varchar(80) DEFAULT NULL,
  `country` varchar(80) DEFAULT NULL,
  `city` varchar(80) DEFAULT NULL,
  `telephone` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_manufacturer`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manufacturer`
--

LOCK TABLES `manufacturer` WRITE;
/*!40000 ALTER TABLE `manufacturer` DISABLE KEYS */;
INSERT INTO `manufacturer` VALUES (1,'adidas','Chine','Pekin','04363553'),(2,'puma','Japan','Tokio','04325356'),(3,'nike','Chine','Xianning','04353664'),(4,'sanuk','Italy','Taranto','03453636'),(5,'mida','Ukraine','Kharkiv','09935738');
/*!40000 ALTER TABLE `manufacturer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `material`
--

DROP TABLE IF EXISTS `material`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `material` (
  `id_material` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  `date_producing` date DEFAULT NULL,
  `id_manufacturer` int(11) NOT NULL,
  `id_material_type` int(11) NOT NULL,
  `id_fashion` int(11) NOT NULL,
  PRIMARY KEY (`id_material`,`id_manufacturer`,`id_material_type`,`id_fashion`),
  KEY `fk_material_manufacturer_idx` (`id_manufacturer`),
  KEY `fk_material_material_type1_idx` (`id_material_type`),
  KEY `fk_material_fashion1_idx` (`id_fashion`),
  CONSTRAINT `fk_material_fashion1` FOREIGN KEY (`id_fashion`) REFERENCES `fashion` (`id_fashion`) ON UPDATE CASCADE,
  CONSTRAINT `fk_material_manufacturer` FOREIGN KEY (`id_manufacturer`) REFERENCES `manufacturer` (`id_manufacturer`) ON UPDATE CASCADE,
  CONSTRAINT `fk_material_material_type1` FOREIGN KEY (`id_material_type`) REFERENCES `material_type` (`id_material_type`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `material`
--

LOCK TABLES `material` WRITE;
/*!40000 ALTER TABLE `material` DISABLE KEYS */;
INSERT INTO `material` VALUES (1,'набойки','2016-11-20',1,1,1),(2,'набойки 2','2016-04-21',2,2,2),(3,'колодки','2016-01-20',1,3,1),(4,'колодки 2','2016-01-20',3,1,1),(5,'набойки 3','2016-03-22',2,2,1),(6,'набойки 4','2015-03-22',1,4,1),(7,'набойки 5','2015-03-22',4,1,2),(44,'fdh','2016-12-12',1,1,1);
/*!40000 ALTER TABLE `material` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `material_type`
--

DROP TABLE IF EXISTS `material_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `material_type` (
  `id_material_type` int(11) NOT NULL,
  `name` varchar(80) DEFAULT NULL,
  `definition` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`id_material_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `material_type`
--

LOCK TABLES `material_type` WRITE;
/*!40000 ALTER TABLE `material_type` DISABLE KEYS */;
INSERT INTO `material_type` VALUES (1,'шкіра','отримана з різної шкіряної сировини'),(2,'штучна тканина','емітує натуральну шкіру'),(3,'гума','для виробництва підошви'),(4,'синтетичні матеріали','для виробництва підошви');
/*!40000 ALTER TABLE `material_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `materialview`
--

DROP TABLE IF EXISTS `materialview`;
/*!50001 DROP VIEW IF EXISTS `materialview`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `materialview` AS SELECT 
 1 AS `id_material`,
 1 AS `name`,
 1 AS `date_producing`,
 1 AS `producer`,
 1 AS `type`,
 1 AS `fason`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `id_order` int(11) NOT NULL,
  `date` date DEFAULT NULL,
  `amount_material` int(11) DEFAULT NULL,
  `id_material` int(11) NOT NULL,
  `id_destination` int(11) NOT NULL,
  `id_state` int(11) NOT NULL,
  PRIMARY KEY (`id_order`,`id_material`,`id_destination`,`id_state`),
  KEY `fk_orders_material1_idx` (`id_material`),
  KEY `fk_orders_destination1_idx` (`id_destination`),
  KEY `fk_orders_state1_idx` (`id_state`),
  CONSTRAINT `fk_orders_destination1` FOREIGN KEY (`id_destination`) REFERENCES `destination` (`id_destination`) ON UPDATE CASCADE,
  CONSTRAINT `fk_orders_material1` FOREIGN KEY (`id_material`) REFERENCES `material` (`id_material`) ON UPDATE CASCADE,
  CONSTRAINT `fk_orders_state1` FOREIGN KEY (`id_state`) REFERENCES `state` (`id_state`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (2,'2016-09-21',5,1,1,1),(3,'2016-10-01',6,1,2,1),(4,'2016-10-01',5,2,1,1),(11,'2016-12-09',1000,1,1,2),(13,'2016-12-09',3,4,1,1),(15,'2016-12-09',34,1,2,1),(17,'2016-12-10',23,1,2,1),(22,'2016-10-01',2,3,1,1),(24,'2016-12-11',2,2,1,1),(25,'2016-12-11',3,4,1,1),(26,'2016-12-11',4,2,1,1),(27,'2016-12-11',11,1,2,1),(28,'2016-12-11',5,3,1,1),(29,'2016-12-11',7,1,1,1),(30,'2016-12-12',400,1,1,2),(31,'2016-12-11',5,2,2,1),(32,'2016-12-12',200,3,1,2),(33,'2016-12-12',11,1,1,1),(34,'2016-12-12',9000,1,1,2),(35,'2016-12-12',444,1,1,2),(36,'2016-12-12',89,4,2,1),(37,'2016-12-12',11,5,1,1),(38,'2016-12-13',23,1,1,1);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `ordersview`
--

DROP TABLE IF EXISTS `ordersview`;
/*!50001 DROP VIEW IF EXISTS `ordersview`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `ordersview` AS SELECT 
 1 AS `id_order`,
 1 AS `date`,
 1 AS `amount_material`,
 1 AS `name`,
 1 AS `type`,
 1 AS `status`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `received_material`
--

DROP TABLE IF EXISTS `received_material`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `received_material` (
  `id_received_material` int(11) NOT NULL,
  `date_received` date DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `id_material` int(11) NOT NULL,
  PRIMARY KEY (`id_received_material`,`id_material`),
  KEY `fk_received_material_material1_idx` (`id_material`),
  CONSTRAINT `fk_received_material_material1` FOREIGN KEY (`id_material`) REFERENCES `material` (`id_material`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `received_material`
--

LOCK TABLES `received_material` WRITE;
/*!40000 ALTER TABLE `received_material` DISABLE KEYS */;
INSERT INTO `received_material` VALUES (1,'2016-09-11',50,1),(2,'2016-09-12',20,2),(3,'2016-09-12',30,3),(4,'2016-11-15',20,1),(5,'2016-12-10',10,2),(6,'2016-12-11',100,4),(7,'2016-09-11',50,1),(8,'2016-09-12',20,2),(9,'2016-09-12',40,5),(10,'2016-11-15',20,6),(11,'2016-12-11',10,7),(12,'2016-12-12',100,4);
/*!40000 ALTER TABLE `received_material` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `receivedview`
--

DROP TABLE IF EXISTS `receivedview`;
/*!50001 DROP VIEW IF EXISTS `receivedview`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `receivedview` AS SELECT 
 1 AS `id_received_material`,
 1 AS `date_received`,
 1 AS `amount`,
 1 AS `name`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `state`
--

DROP TABLE IF EXISTS `state`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `state` (
  `id_state` int(11) NOT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_state`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `state`
--

LOCK TABLES `state` WRITE;
/*!40000 ALTER TABLE `state` DISABLE KEYS */;
INSERT INTO `state` VALUES (1,'виконано'),(2,'не виконано');
/*!40000 ALTER TABLE `state` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `idusers` int(11) NOT NULL,
  `login` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `type` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idusers`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'leo','1234','admin'),(2,'leon','1111','user');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'depotdb'
--

--
-- Dumping routines for database 'depotdb'
--

--
-- Final view structure for view `issued_materials`
--

/*!50001 DROP VIEW IF EXISTS `issued_materials`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `issued_materials` AS (select `orders`.`id_order` AS `id_issued_material`,`orders`.`date` AS `date`,`orders`.`amount_material` AS `amount_material`,`orders`.`id_material` AS `id_material`,`orders`.`id_destination` AS `id_destination` from `orders` where (`orders`.`id_state` = 1)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `issuedview`
--

/*!50001 DROP VIEW IF EXISTS `issuedview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `issuedview` AS (select `issued_materials`.`id_issued_material` AS `id_issued_material`,`issued_materials`.`date` AS `date`,`issued_materials`.`amount_material` AS `amount_material`,`material`.`name` AS `name`,`destination`.`type` AS `type` from ((`issued_materials` join `material` on((`material`.`id_material` = `issued_materials`.`id_material`))) join `destination` on((`destination`.`id_destination` = `issued_materials`.`id_destination`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `materialview`
--

/*!50001 DROP VIEW IF EXISTS `materialview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `materialview` AS (select `material`.`id_material` AS `id_material`,`material`.`name` AS `name`,`material`.`date_producing` AS `date_producing`,`manufacturer`.`name` AS `producer`,`material_type`.`name` AS `type`,`fashion`.`name` AS `fason` from (((`material` join `manufacturer` on((`material`.`id_manufacturer` = `manufacturer`.`id_manufacturer`))) join `material_type` on((`material`.`id_material_type` = `material_type`.`id_material_type`))) join `fashion` on((`material`.`id_fashion` = `fashion`.`id_fashion`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `ordersview`
--

/*!50001 DROP VIEW IF EXISTS `ordersview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `ordersview` AS (select `orders`.`id_order` AS `id_order`,`orders`.`date` AS `date`,`orders`.`amount_material` AS `amount_material`,`material`.`name` AS `name`,`destination`.`type` AS `type`,`state`.`name` AS `status` from (((`orders` join `material` on((`material`.`id_material` = `orders`.`id_material`))) join `destination` on((`destination`.`id_destination` = `orders`.`id_destination`))) join `state` on((`state`.`id_state` = `orders`.`id_state`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `receivedview`
--

/*!50001 DROP VIEW IF EXISTS `receivedview`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `receivedview` AS (select `received_material`.`id_received_material` AS `id_received_material`,`received_material`.`date_received` AS `date_received`,`received_material`.`amount` AS `amount`,`material`.`name` AS `name` from (`received_material` join `material` on((`material`.`id_material` = `received_material`.`id_material`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-12-13 21:09:03
